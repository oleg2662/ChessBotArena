namespace BoardGame.Model.Api.AccountControllerModels
{
    /// <summary>
    /// The contract for the login.
    /// </summary>
    public interface ILoginModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        string UserName { get; set; }
    }
}