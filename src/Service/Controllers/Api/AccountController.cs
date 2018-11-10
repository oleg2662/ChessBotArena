using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using BoardGame.Service.Extensions;
using BoardGame.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Api.AccountControllerModels;

namespace BoardGame.Service.Controllers.Api
{
    /// <summary>
    /// Controller used to manage logins.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager repository used to query user data and check for user existence.</param>
        /// <param name="signInManager">The sign-in manager repository used to check username and password validity.</param>
        /// <param name="logger">The logger used to log events in the controller.</param>
        /// <param name="configuration">The configuration provider.</param>
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Action used to check the login and return the JWT token if successful.
        /// </summary>
        /// <param name="model">The login model with the username and password.</param>
        /// <returns>Returns the JWT token if successful. Otherwise a HTTP error.</returns>
        /// <response code="200">If it's successful with a JWT token.</response>
        /// <response code="401">If there is an authentication error.</response>
        /// <response code="500">If there is a server error.</response>
        [ProducesResponseType(typeof(JwtSecurityToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                _logger.LogInformation("User login denied.");
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                _logger.LogInformation("User login denied.");
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("IsBot", user.Bot.ToString()),
                new Claim("OriginallyIssuedAt", DateTime.UtcNow.Ticks.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecurityKey()));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetBaseUrl(),
                audience: _configuration.GetBaseUrl(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}