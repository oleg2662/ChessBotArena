using System.Collections.Generic;
using System.Net;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Controllers.Api
{
    /// <summary>
    /// Controller used to return the ladder.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LadderController : BaseController
    {
        private readonly ILadderRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LadderController" /> class.
        /// </summary>
        /// <param name="repository">The ladder repository used to query the data from the database.</param>
        public LadderController(ILadderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns the whole ladder of the game.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        [ProducesResponseType(typeof(IEnumerable<LadderItem>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetLadder()
        {
            var result = _repository.GetLadder();
            return Ok(result);
        }

        /// <summary>
        /// Returns the ladder of bot players.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        [ProducesResponseType(typeof(IEnumerable<LadderItem>), (int)HttpStatusCode.OK)]
        [HttpGet("bots")]
        public IActionResult GetBotLadder()
        {
            var result = _repository.GetBotLadder();
            return Ok(result);
        }

        /// <summary>
        /// Returns the ladder of human players.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        [ProducesResponseType(typeof(IEnumerable<LadderItem>), (int)HttpStatusCode.OK)]
        [HttpGet("human")]
        public IActionResult GetHumanPlayerLadder()
        {
            var result = _repository.GetHumanLadder();
            return Ok(result);
        }
    }
}