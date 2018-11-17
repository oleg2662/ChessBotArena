using System;
using BoardGame.Game.Abstraction;
using BoardGame.Game.Chess.Extensions;
using Newtonsoft.Json;

namespace BoardGame.Game.Chess.Pieces
{
    /// <summary>
    /// Base abstract class of a chess piece.
    /// </summary>
    [Serializable]
    public abstract class ChessPiece : ICloneable<ChessPiece>
    {
        protected readonly PieceKind OriginalPieceKind;

        /// <summary>
        /// Gets the owner of the chess piece.
        /// </summary>
        public ChessPlayer Owner { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the chess piece has already been  moved in the game.
        /// </summary>
        public bool HasMoved { get; set; }

        /// <summary>
        /// Gets or sets the kind of the chess piece.
        /// </summary>
        public PieceKind Kind { get; set; }

        protected ChessPiece(ChessPlayer owner, PieceKind pieceKind)
            : this(owner, pieceKind, false)
        {
        }

        [JsonConstructor]
        protected ChessPiece(ChessPlayer owner, PieceKind pieceKind, bool hasMoved)
        {
            Owner = owner;
            Kind = pieceKind;
            HasMoved = hasMoved;
            OriginalPieceKind = pieceKind;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;

                hash = (hash ^ Constants.HashXor) ^ Owner.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ OriginalPieceKind.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return Kind.ToFigure(Owner);
        }

        public abstract ChessPiece Clone();
    }
}
