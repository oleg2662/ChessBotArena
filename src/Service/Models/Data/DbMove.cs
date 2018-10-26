//using System;
//using System.ComponentModel.DataAnnotations;
//using Game.Chess;
//using Game.Chess.Moves;
//using Game.Chess.Pieces;

//using CastlingTypeEnum = Game.Chess.Moves.CastlingType;

//namespace BoardGame.Service.Models.Data
//{
//    /// <inheritdoc />
//    public class DbMove : IDbMove
//    {
//        /// <inheritdoc />
//        [Key]
//        public Guid MoveId { get; set; }

//        /// <inheritdoc />
//        public Position From { get; set; }

//        /// <inheritdoc />
//        public Position To { get; set; }

//        /// <inheritdoc />
//        public bool IsCaptureMove { get; set; }

//        /// <inheritdoc />
//        public ChessPlayer Owner { get; set; }

//        /// <inheritdoc />
//        public PieceKind ChessPiece { get; set; }

//        /// <inheritdoc />
//        public ChessMoveResult ChessMoveResult { get; set; }

//        /// <inheritdoc />
//        public PieceKind PromoteTo { get; set; }

//        /// <inheritdoc />
//        public Position CapturePosition { get; set; }

//        /// <inheritdoc />
//        public Position RookFrom
//        {
//            get
//            {
//                switch (CastlingType.Value)
//                {
//                    case CastlingTypeEnum.Long:
//                        return new Position('A', From.Row);
//                    case CastlingTypeEnum.Short:
//                        return new Position('H', From.Row);
//                    default:
//                        return null;
//                }
//            }
//        }

//        /// <inheritdoc />
//        public Position RookTo
//        {
//            get
//            {
//                switch (this.CastlingType.Value)
//                {
//                    case CastlingTypeEnum.Long:
//                        return new Position('A', From.Row);
//                    case CastlingTypeEnum.Short:
//                        return new Position('D', From.Row);
//                    default:
//                        return null;
//                }
//            }
//        }

//        /// <inheritdoc />
//        public CastlingTypeEnum? CastlingType { get; set; }

//        /// <inheritdoc />
//        public DateTime CreatedDate { get; set; }
//    }
//}
