using System.Collections.Generic;
using BoardGame.Model.Api.LadderControllerModels;

namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// The repository presenting the player ladder.
    /// </summary>
    public interface ILadderRepository
    {
        /// <summary>
        /// Gets the actual ladder.
        /// </summary>
        /// <returns>The combined ladder of bot and human players.</returns>
        IEnumerable<LadderItem> GetLadder();

        /// <summary>
        /// Gets the actual ladder of bots.
        /// </summary>
        /// <returns>Returns the actual ladder of bot players.</returns>
        IEnumerable<LadderItem> GetBotLadder();

        /// <summary>
        /// Gets the actual ladder of human players.
        /// </summary>
        /// <returns>Returns the actual ladder of human players.</returns>
        IEnumerable<LadderItem> GetHumanLadder();
    }
}
