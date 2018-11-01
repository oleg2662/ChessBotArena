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
        Ok = 1,

        /// <summary>
        /// It's not your turn!
        /// </summary>
        WrongTurn = -1,

        /// <summary>
        /// Invalid move!
        /// </summary>
        InvalidMove = -2,

        /// <summary>
        /// Couldn't find match!
        /// </summary>
        NoMatchFound = -3,

        /// <summary>
        /// Multiple matches found. (DB error)
        /// </summary>
        MultipleMatchesFound = -4,
    }
}
