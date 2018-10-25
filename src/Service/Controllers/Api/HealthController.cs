namespace BoardGame.Service.Controllers.Api
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;


    /// <summary>
    /// Controller implementation of a simple healthcheck.
    /// If the system is up and running it will answer to requests with the current application version number.
    /// </summary>
    [Route("api/Health")]
    public class HealthController : Controller
    {
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

            return this.Ok(version);
        }
    }
}