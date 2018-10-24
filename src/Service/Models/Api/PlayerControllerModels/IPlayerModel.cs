namespace BoardGame.Service.Models.Api.PlayerControllerModels
{
    public interface IPlayerModel
    {
        bool IsBot { get; set; }
        string Name { get; set; }
    }
}