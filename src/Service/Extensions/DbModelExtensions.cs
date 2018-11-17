using System.Collections.Generic;
using BoardGame.Game.Chess;
using BoardGame.Service.Models.Data;

namespace BoardGame.Service.Extensions
{
    /// <summary>
    /// Contains extension methods of the DB model classes
    /// </summary>
    public static class DbModelExtensions
    {
        /// <summary>
        /// Gets a dictionary of player usernames based on their playing colour in the game.
        /// </summary>
        /// <param name="game">The chess game coming from database.</param>
        /// <returns>Dictionary of game players.</returns>
        public static Dictionary<ChessPlayer, string> GetPlayerNames(this DbChessGame game)
        {
            var dictionary = new Dictionary<ChessPlayer, string>(2);
            dictionary.Add(ChessPlayer.White, game.WhitePlayer.UserName);
            dictionary.Add(ChessPlayer.Black, game.BlackPlayer.UserName);

            return dictionary;
        }
    }
}
