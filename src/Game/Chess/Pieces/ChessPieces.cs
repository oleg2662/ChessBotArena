using System;

namespace Game.Chess.Pieces
{
    /// <summary>
    /// Shorthand for the creation of different chesspieces.
    /// </summary>
    public static class ChessPieces
    {
        #region Coloured chesspieces

        /// <summary>
        /// Creates and returns a new white bishop.
        /// </summary>
        public static Bishop WhiteBishop => new Bishop(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new white king.
        /// </summary>
        public static King WhiteKing => new King(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new white Knight.
        /// </summary>
        public static Knight WhiteKnight => new Knight(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new white Pawn.
        /// </summary>
        public static Pawn WhitePawn => new Pawn(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new white Queen.
        /// </summary>
        public static Queen WhiteQueen => new Queen(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new white Rook.
        /// </summary>
        public static Rook WhiteRook => new Rook(ChessPlayer.White);

        /// <summary>
        /// Creates and returns a new black bishop.
        /// </summary>
        public static Bishop BlackBishop => new Bishop(ChessPlayer.Black);

        /// <summary>
        /// Creates and returns a new black King.
        /// </summary>
        public static King BlackKing => new King(ChessPlayer.Black);

        /// <summary>
        /// Creates and returns a new black Knight.
        /// </summary>
        public static Knight BlackKnight => new Knight(ChessPlayer.Black);

        /// <summary>
        /// Creates and returns a new black Pawn.
        /// </summary>
        public static Pawn BlackPawn => new Pawn(ChessPlayer.Black);

        /// <summary>
        /// Creates and returns a new black Queen.
        /// </summary>
        public static Queen BlackQueen => new Queen(ChessPlayer.Black);

        /// <summary>
        /// Creates and returns a new black Rook.
        /// </summary>
        public static Rook BlackRook => new Rook(ChessPlayer.Black);

        #endregion

        #region Creation of general chesspieces

        /// <summary>
        /// Creates and returns a new Bishop.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static Bishop Bishop(ChessPlayer player, bool hasMoved = false) => new Bishop(player, hasMoved);

        /// <summary>
        /// Creates and returns a new King.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static King King(ChessPlayer player, bool hasMoved = false) => new King(player, hasMoved);

        /// <summary>
        /// Creates and returns a new Knight.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static Knight Knight(ChessPlayer player, bool hasMoved = false) => new Knight(player, hasMoved);

        /// <summary>
        /// Creates and returns a new Pawn.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static Pawn Pawn(ChessPlayer player, bool hasMoved = false) => new Pawn(player, hasMoved);

        /// <summary>
        /// Creates and returns a new Queen.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static Queen Queen(ChessPlayer player, bool hasMoved = false) => new Queen(player, hasMoved);

        /// <summary>
        /// Creates and returns a new Rook.
        /// </summary>
        /// <param name="player">The owner player.</param>
        /// <param name="hasMoved">Sets whether the piece has been moved since it's creation.</param>
        public static Rook Rook(ChessPlayer player, bool hasMoved = false) => new Rook(player, hasMoved);

        #endregion

        public static ChessPiece Create(PieceKind kind, ChessPlayer player, bool hasMoved = false)
        {
            var black = player == ChessPlayer.Black;

            switch (kind)
            {
                case PieceKind.King:
                    return black ? BlackKing : WhiteKing;
                case PieceKind.Queen:
                    return black ? BlackQueen : WhiteQueen;
                case PieceKind.Rook:
                    return black ? BlackRook : WhiteRook;
                case PieceKind.Bishop:
                    return black ? BlackBishop : WhiteBishop;
                case PieceKind.Knight:
                    return black ? BlackKnight : WhiteKnight;
                case PieceKind.Pawn:
                    return black ? BlackPawn : WhitePawn;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }
    }
}
