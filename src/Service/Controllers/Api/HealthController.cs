namespace BoardGame.Service.Controllers.Api
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    //[Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        /// <summary>
        /// Health check. Returns the version number of the service.
        /// </summary>
        /// <returns>The version information of the service.</returns>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var version = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;
            return this.Ok(version);
        }
    }
}