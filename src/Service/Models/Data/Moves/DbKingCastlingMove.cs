using Game.Chess;
using Game.Chess.Extensions;
using Game.Chess.Moves;
using System;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Represents a castling move in the database.
    /// </summary>
    [Serializable]
    public sealed class DbKingCastlingMove : DbChessMove
    {
        /// <summary>
        /// Gets the source position of the rook's move.
        /// Calculated based on the owner and the type of the castling.
        /// </summary>
        public Position RookFrom
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return new Position('A', FromRow);
                    case CastlingType.Short:
                        return new Position('H', FromRow);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }

        /// <summary>
        /// Gets the destination position of the rook's move.
        /// Calculated based on the owner and the type of the castling.
        /// </summary>
        public Position RookTo
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return new Position('A', FromRow);
                    case CastlingType.Short:
                        return new Position('D', FromRow);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }

        /// <summary>
        /// Gets the destination position of the move.
        /// At castling it is the move of the king so the king's destination will be here.
        /// </summary>
        public override Position To
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return From.East(2);
                    case CastlingType.Short:
                        return From.West(2);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }

        /// <summary>
        /// Gets or sets the type of the castling.
        /// It can be short or long.
        /// </summary>
        public CastlingType CastlingType { get; set; }
    }
}
