﻿namespace BoardGame.Service.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using BoardGame.Service.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using BoardGame.Service.Models.Api.PlayerControllerModels;

    /// <summary>
    /// Controller used to list the available players in the game.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/Players")]
    public class PlayersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="userManager">The manager of the users.</param>
        /// <param name="logger">The logger used to log errors and warnings.</param>
        public PlayersController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Health check. Returns the version number of the service.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        /// <response code="200">Returns the list of registered users.</response>
        /// <response code="401">If there is an authentication error.</response>
        /// <response code="500">If there is a server error.</response>
        [ProducesResponseType(typeof(IEnumerable<Player>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        public IActionResult GetPlayers()
        {
            var listOfPlayers = this.userManager.Users
                .Select(x => new Player
                {
                    Name = x.UserName,
                    IsBot = x.Bot
                }).AsEnumerable();
            return Ok(listOfPlayers);
        }
    }
}