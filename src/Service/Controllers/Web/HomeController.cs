using System.Diagnostics;
using BoardGame.Service.Models.Web;
using BoardGame.Service.Models.Web.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BoardGame.Service.Controllers.Web
{
    /// <summary>
    /// Web controller for the front page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="config">The configuration provider.</param>
        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Returns the view for the index page.
        /// </summary>
        /// <returns>The view for the index page.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            var result = new IndexViewModel
            {
                BaseUrl = _config["BaseUrl"]
            };

            return View(result);
        }

        /// <summary>
        /// Returns the view for the error messages.
        /// </summary>
        /// <returns>The error messages.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
