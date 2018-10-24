namespace BoardGame.Service.Models.Api.AccountControllerModels
{
    public class LoginModel : ILoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
