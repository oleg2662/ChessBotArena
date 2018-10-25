namespace BoardGame.Service.Models
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is a bot or not.
        /// </summary>
        public bool Bot { get; set; }
    }
}
