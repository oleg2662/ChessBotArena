using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers.Api
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
    using BoardGame.Service.Models.Api.ChessGamesControllerModels;

    /// <summary>
    /// Controller used to list the available players in the game.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/Games")]
    public class ChessGamesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGamesController"/> class.
        /// </summary>
        /// <param name="userManager">The manager of the users.</param>
        /// <param name="logger">The logger used to log errors and warnings.</param>
        public ChessGamesController(
            UserManager<ApplicationUser> userManager,
            ILogger<ChessGamesController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Returns the list of chess games for the current user.
        /// </summary>
        /// <returns>List of games for the current user.</returns>
        /// <response code="200">Returns the list of games of the current user..</response>
        /// <response code="401">If there is an authentication error.</response>
        /// <response code="500">If there is a server error.</response>
        [ProducesResponseType(typeof(IEnumerable<ChessGame>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        public IActionResult GetChessGames()
        {
            return Ok(new ChessGame[] { });
        }
    }
}
