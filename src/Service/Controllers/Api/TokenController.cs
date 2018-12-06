using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

using BoardGame.Service.Extensions;
using BoardGame.Service.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BoardGame.Service.Controllers.Api
{
    /// <summary>
    /// Controller used to manage the JWT tokens.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : BaseController
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="userManager">The manager of the users.</param>
        /// <param name="logger">The logger used to log errors and warnings.</param>
        /// <param name="configuration">The configuration provider.</param>
        public TokenController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Prolongs the current token.
        /// </summary>
        /// <returns>A prolonged JSON token.</returns>
        /// <response code="200">If it's successful with a prolonged JWT token.</response>
        /// <response code="400">If there is an authentication error.</response>
        /// <response code="401">If there is an authentication error.</response>
        /// <response code="500">If there is a server error.</response>
        [ProducesResponseType(typeof(JwtSecurityToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public IActionResult ProlongToken()
        {
            var tokenString = Request.Headers["Authorization"].FirstOrDefault()?.Remove(0, 7);

            var token = new JwtSecurityToken(tokenString);

            var originallyIssuedAtTicksList = token.Claims
                .Where(x => x.Type == "OriginallyIssuedAt")
                .Select(x => x.Value)
                .ToList();

            if (originallyIssuedAtTicksList.Count != 1)
            {
                _logger.LogWarning($"{GetCurrentUser()} has tried to prolong a token but the supplied token string was invalid.(OriginallyIssuedAt)");
                return BadRequest("Invalid token provided.");
            }

            if (!long.TryParse(originallyIssuedAtTicksList.First(), out long originallyIssuedAtTicks))
            {
                _logger.LogWarning($"{GetCurrentUser()} has tried to prolong a token but the supplied token string was invalid.(OriginallyIssuedAt try parse.)");
                return BadRequest("Invalid token provided.");
            }

            var originallyIssuedAt = new DateTime(originallyIssuedAtTicks, DateTimeKind.Utc);

            var ageOfOriginalToken = (DateTime.UtcNow - originallyIssuedAt);

            if (ageOfOriginalToken > TimeSpan.FromDays(1))
            {
                _logger.LogWarning($"{GetCurrentUser()} has tried to prolong a token but the supplied token was too old.");
                return Forbid();
            }

            var tokenUserNames = token.Claims
                .Where(x => x.Type == ClaimTypes.Name)
                .Select(x => x.Value).ToList();

            if (tokenUserNames.Count != 1)
            {
                _logger.LogWarning($"{GetCurrentUser()} has tried to prolong a token but the supplied token string was invalid.(User name)");
                return BadRequest("Invalid token provided.");
            }

            var tokenUserName = tokenUserNames.First();

            var user = _userManager.Users
                .FirstOrDefault(x => x.UserName == tokenUserName);

            if (user == null)
            {
                _logger.LogWarning($"{GetCurrentUser()} has tried to prolong a token but the supplied token string was invalid.(User null.)");
                return Forbid();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("IsBot", user.Bot.ToString()),
                new Claim("OriginallyIssuedAt", DateTime.UtcNow.Ticks.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecurityKey()));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(30);

            var newToken = new JwtSecurityToken(
                issuer: _configuration.GetBaseUrl(),
                audience: _configuration.GetBaseUrl(),
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            _logger.LogInformation($"{GetCurrentUser()} has asked for a prolonged token.");

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(newToken)
            });
        }
    }
}