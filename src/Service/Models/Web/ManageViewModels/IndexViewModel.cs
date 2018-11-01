using System.ComponentModel.DataAnnotations;
using BoardGame.Service.Controllers.Web;

namespace BoardGame.Service.Models.Web.ManageViewModels
{
    /// <summary>
    /// Gets or sets the user management model in the <see cref="ManageController"/>.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's e-mail address has already been confirmed.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the e-mail address.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the status message of the operation.
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account is a bot account.
        /// </summary>
        public bool Bot { get; set; }
    }
}
