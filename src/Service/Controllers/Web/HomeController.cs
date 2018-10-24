namespace BoardGame.Service.Controllers.Web
{
    using System.Diagnostics;
    using BoardGame.Service.Models;
    using BoardGame.Service.Models.Web;
    using BoardGame.Service.Models.Web.HomeViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class HomeController : Controller
    {
        private readonly IConfiguration config;

        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Manage");
            }

            var result = new IndexViewModel
            {
                BaseUrl = this.config["BaseUrl"]
            };

            return this.View(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
