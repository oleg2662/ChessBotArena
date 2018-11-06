using System.Threading;
using Microsoft.Extensions.Logging;

namespace BoardGame.Service.Controllers.Api
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller implementation of a simple health check.
    /// If the system is up and running it will answer to requests with the current application version number.
    /// </summary>
    [Route("api/Health")]
    public class HealthController : BaseController
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthController" /> class.
        /// </summary>
        /// <param name="logger">The logger used to log events in the controller.</param>
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Health check. Returns the version number of the service.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult Get()
        {
            var version = Microsoft.Extensions
                                   .PlatformAbstractions
                                   .PlatformServices
                                   .Default
                                   .Application
                                   .ApplicationVersion;

            _logger.LogInformation($"{GetCurrentUserNameOrAnonymous()} has queried the health status.");
            return Ok(version);
        }

        private string GetCurrentUserNameOrAnonymous()
        {
            var userNameInner = (GetCurrentUser() ?? string.Empty).Trim();
            var userName = userNameInner == string.Empty ? "{{{ANONYMOUS}}}" : userNameInner;
            return userName;
        }
    }
}