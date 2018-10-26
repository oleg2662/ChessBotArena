using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BoardGame.Service.Extensions;
using BoardGame.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using BoardGame.Service.Models.Api.AccountControllerModels;

namespace BoardGame.Service.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody] LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                this.logger.LogInformation("User login denied.");
                return this.Unauthorized();
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                this.logger.LogInformation("User login denied.");
                return this.Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("IsBot", user.Bot.ToString()),
                new Claim("OriginallyIssuedAt", DateTime.UtcNow.Ticks.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSecurityKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration.GetBaseUrl(),
                audience: configuration.GetBaseUrl(),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return this.Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}