namespace BoardGame.Game.Chess.Moves
{
    /// <summary>
    /// Represents special moves.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Resign. Game ends.
        /// </summary>
        Resign = 0,

        /// <summary>
        /// Offer a draw.
        /// </summary>
        DrawOffer,

        /// <summary>
        /// Accept a draw. Game ends.
        /// </summary>
        DrawAccept,

        /// <summary>
        /// Decline a draw. Game continues.
        /// </summary>
        DrawDecline
    }
}
