namespace BoardGame.Service.Controllers.Api
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using BoardGame.Service.Extensions;
    using Microsoft.AspNetCore.Mvc;

    using BoardGame.Service.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly ILogger logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public TokenController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> ProlongToken([FromBody] string tokenString)
        {
            var token = new JwtSecurityToken(tokenString);

            var originallyIssuedAtTicksList = token.Claims
                .Where(x => x.Type == "OriginallyIssuedAt")
                .Select(x => x.Value)
                .ToList();

            if (originallyIssuedAtTicksList.Count != 1)
            {
                return this.BadRequest("Invalid token provided.");
            }

            if (!long.TryParse(originallyIssuedAtTicksList.First(), out long originallyIssuedAtTicks))
            {
                return this.BadRequest("Invalid token provided.");
            }

            var originallyIssuedAt = new DateTime(originallyIssuedAtTicks, DateTimeKind.Utc);

            var ageOfOriginalToken = (DateTime.UtcNow - originallyIssuedAt);

            if (ageOfOriginalToken > TimeSpan.FromDays(1))
            {
                // Too old token.
                return this.Forbid();
            }

            var tokenUserNames = token.Claims
                .Where(x => x.Type == ClaimTypes.Name)
                .Select(x => x.Value).ToList();

            if (tokenUserNames.Count != 1)
            {
                return this.BadRequest("Invalid token provided.");
            }

            var tokenUserName = tokenUserNames.First();

            var user = this.userManager.Users
                .FirstOrDefault(x => x.UserName == tokenUserName);

            if (user == null)
            {
                return this.Forbid();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("IsBot", user.Bot.ToString()),
                new Claim("OriginallyIssuedAt", DateTime.UtcNow.Ticks.ToString())
            };

            var currentDomain = this.HttpContext.Request.Host.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSecurityKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var newToken = new JwtSecurityToken(
                issuer: this.configuration.GetBaseUrl(),
                audience: this.configuration.GetBaseUrl(),
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return this.Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(newToken)
            });
        }
    }
}