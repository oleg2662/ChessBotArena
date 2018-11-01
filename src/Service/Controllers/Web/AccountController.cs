using System.Threading.Tasks;
using BoardGame.Service.Models;
using BoardGame.Service.Models.Web.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGame.Service.Controllers.Web
{
    /// <summary>
    /// Web controller for the account management. (Registration, login.)
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager repository used to query user data and check for user existence.</param>
        /// <param name="signInManager">The sign-in manager repository used to check username and password validity.</param>
        /// <param name="logger">The logger used to log events in the controller.</param>
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Returns the view for the empty login screen.
        /// </summary>
        /// <param name="returnUrl">URL where the user will be redirected after a successful login.</param>
        /// <returns>Return the login view.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Returns the login screen after supplying the required information for the login procedure.
        /// </summary>
        /// <param name="model">The required information for the login.</param>
        /// <param name="returnUrl">URL where the user will be redirected after a successful login.</param>
        /// <returns>The login result view.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToAction(nameof(Lockout));
            }

            _logger.LogWarning("Invalid login attempt.");
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);

            // If we got this far, something failed, redisplay form
        }

        /// <summary>
        /// Returns the view for the lockout screen which is displayed if the user who tried to login is locked out.
        /// </summary>
        /// <returns>The view for the lockout screen.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// Returns the view for the new user registration.
        /// </summary>
        /// <param name="returnUrl">URL where the user will be redirected after successful registration and login.</param>
        /// <returns>The empty view for the new user registration.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Returns the view for the new user registration after supplying the required information for the registration procedure.
        /// </summary>
        /// <param name="model">The information required to register a new user.</param>
        /// <param name="returnUrl">URL where the user will be redirected after successful registration and login.</param>
        /// <returns>The view for the result of the registration.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Bot = model.Bot };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                _logger.LogInformation("User created a new account with password.");
                return RedirectToLocal(returnUrl);
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            _logger.LogWarning("User registration failed.");
            return View(model);
        }

        /// <summary>
        /// Returns the view for the logout screen.
        /// </summary>
        /// <returns>The view for the logout screen.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Returns the view for the access denied screen.
        /// </summary>
        /// <returns>The view for the access denied screen.</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
