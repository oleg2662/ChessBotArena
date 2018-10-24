namespace BoardGame.Service.Models.Api.AccountControllerModels
{
    public interface ILoginModel
    {
        string Password { get; set; }
        string UserName { get; set; }
    }
}