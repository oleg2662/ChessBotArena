using BoardGame.Service.Models.Api.ChessGamesControllerModels;
using BoardGame.Service.Models.Data;
using BoardGame.Service.Models.Data.Moves;
using Game.Chess.Moves;

namespace BoardGame.Service.Models.Converters
{
    /// <summary>
    /// Interface of the DB - API model converters used by the chess game repository.
    /// </summary>
    public interface IChessGameRepositoryConverter
    {
        /// <summary>
        /// Converts the DB side object to the API target type.
        /// </summary>
        /// <param name="source">Source object.</param>
        /// <returns>Null if the source is null. Otherwise the converted version.</returns>
        ChessGame ConvertToChessGame(DbChessGame source);

        /// <summary>
        /// Converts the DB side chess game object to the API side detailed object model.
        /// </summary>
        /// <param name="source">DB side source object.</param>
        /// <returns>API side detailed object.</returns>
        ChessGameDetails ConvertToChessGameDetails(DbChessGame source);

        /// <summary>
        /// Converts the DB side user to a minimal information player dto used in the API.
        /// </summary>
        /// <param name="source">The full detailed application user object.</param>
        /// <returns>The chess game player dto used in the API.</returns>
        ChessGamePlayerDto ConvertUser(ApplicationUser source);

        /// <summary>
        /// Converts a move from the database to chess move.
        /// </summary>
        /// <param name="dbMove">The move from the database</param>
        /// <returns>Chess move.</returns>
        BaseMove CovertToChessMove(DbBaseMove dbMove);

        /// <summary>
        /// Converts a move to the database chess move.
        /// </summary>
        /// <param name="move">The normal move</param>
        /// <returns>Database chess move.</returns>
        DbBaseMove CovertToDbChessMove(BaseMove move);
    }
}
