using Game.Chess;
using Game.Chess.Extensions;
using Game.Chess.Moves;
using System;

namespace BoardGame.Service.Models.Data.Moves
{
    [Serializable]
    public sealed class DbKingCastlingMove : DbChessMove
    {
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

        public CastlingType CastlingType { get; set; }
    }
}
