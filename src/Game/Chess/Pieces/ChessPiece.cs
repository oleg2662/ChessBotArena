using Game.Abstraction;
using System;
using Game.Chess.Extensions;

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
            Owner = owner;
            Kind = pieceKind;
            HasMoved = hasMoved;
        }

        public bool Equals(ChessPiece other)
        {
            if(other is null)
            {
                return false;
            }

            return other.Owner == Owner
                    && other.Kind == Kind;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ChessPiece;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;

                hash = (hash ^ Constants.HashXor) ^ Owner.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ Kind.GetHashCode();

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
