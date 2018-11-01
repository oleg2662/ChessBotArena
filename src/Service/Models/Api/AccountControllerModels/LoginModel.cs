namespace BoardGame.Service.Models.Api.AccountControllerModels
{
    /// <inheritdoc />
    public class LoginModel : ILoginModel
    {
        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }
    }
}
