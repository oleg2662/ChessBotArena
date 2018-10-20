using Game.Abstraction;
using System;

namespace Game.Chess.Pieces
{
    /// <summary>
    /// Base abstract class of a chess piece.
    /// </summary>
    [Serializable]
    public abstract class ChessPiece : IEquatable<ChessPiece>, ICloneable<ChessPiece>
    {
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

        protected ChessPiece(ChessPlayer owner, PieceKind pieceKind, bool hasMoved)
        {
            this.Owner = owner;
            this.Kind = pieceKind;
            this.HasMoved = hasMoved;
        }

        public bool Equals(ChessPiece other)
        {
            if(other is null)
            {
                return false;
            }

            return other.Owner == this.Owner
                    && other.Kind == this.Kind;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ChessPiece;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;

                hash = (hash ^ Constants.HashXor) ^ this.Owner.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ this.Kind.GetHashCode();

                return hash;
            }
        }

        public abstract ChessPiece Clone();
    }
}
