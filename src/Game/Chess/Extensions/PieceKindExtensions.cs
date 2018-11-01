using System;
using Game.Chess.Pieces;

namespace Game.Chess.Extensions
{
    /// <summary>
    /// Contains extension methods of chess pieces
    /// </summary>
    public static class PieceKindExtensions
    {
        /// <summary>
        /// Returns a string representation of the given chess piece.
        /// </summary>
        /// <param name="pieceKind">Type of the piece</param>
        /// <param name="player">The owner player which defines the colour.</param>
        /// <returns>The figure character of the chess piece.</returns>
        public static string ToFigure(this PieceKind pieceKind, ChessPlayer player)
        {
            switch (pieceKind)
            {
                case PieceKind.King:
                    return player == ChessPlayer.White ? "♔ " : "♚";
                case PieceKind.Queen:
                    return player == ChessPlayer.White ? "♕" : "♛";
                case PieceKind.Rook:
                    return player == ChessPlayer.White ? "♖" : "♜";
                case PieceKind.Bishop:
                    return player == ChessPlayer.White ? "♗" : "♝";
                case PieceKind.Knight:
                    return player == ChessPlayer.White ? "♘" : "♞";
                case PieceKind.Pawn:
                    return player == ChessPlayer.White ? "♙" : "♟";
                default:
                    throw new ArgumentOutOfRangeException(nameof(pieceKind));
            }
        }

        /// <summary>
        /// Returns a string representation of the given chess piece.
        /// </summary>
        /// <param name="pieceKind">Type of the piece</param>
        /// <param name="player">The owner player which defines the colour.</param>
        /// <returns>The figure character of the chess piece. If null, returns empty string.</returns>
        public static string ToFigure(this PieceKind? pieceKind, ChessPlayer player)
        {
            return pieceKind.HasValue
                    ? pieceKind.Value.ToFigure(player)
                    : string.Empty;
        }

        /// <summary>
        /// Returns a string representation of the given chess piece.
        /// </summary>
        /// <param name="chessPiece">The chess piece.</param>
        /// <returns>The figure character of the chess piece. If null, returns empty string.</returns>
        public static string ToFigure(this ChessPiece chessPiece)
        {
            var pieceKind = chessPiece?.Kind;
            var owner = chessPiece?.Owner ?? ChessPlayer.Black;

            return pieceKind.HasValue
                    ? pieceKind.Value.ToFigure(owner)
                    : string.Empty;
        }
    }
}
