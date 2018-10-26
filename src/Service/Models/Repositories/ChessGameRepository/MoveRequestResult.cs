using BoardGame.Service.Repositories;

namespace BoardGame.Service.Models.Repositories.ChessGameRepository
{
    /// <summary>
    /// Validation results for the <see cref="IChessGameRepository" />'s move.
    /// </summary>
    public enum MoveRequestResults
    {
        /// <summary>
        /// Move is valid and applied.
        /// </summary>
        OK = 1,

        /// <summary>
        /// It's not your turn!
        /// </summary>
        WrongTurn,

        /// <summary>
        /// Invalid move!
        /// </summary>
        InvalidMove
    }
}
