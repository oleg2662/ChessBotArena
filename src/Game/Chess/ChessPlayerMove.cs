//namespace Game.Chess
//{
//    using Game.Abstraction;
//    using Game.Chess.Moves;
//    using Game.Chess.Pieces;
//    using System;

//    public class ChessPlayerMove : IPlayerMove<ChessPlayer, ChessMove>, IEquatable<ChessPlayerMove>
//    {
//        public ChessPlayer Owner { get; set; }

//        public ChessMove Move { get; set; }

//        public static bool operator ==(ChessPlayerMove x, ChessPlayerMove y)
//        {
//            if (ReferenceEquals(x, y))
//            {
//                return true;
//            }

//            if(x == null && y == null)
//            {
//                return true;
//            }

//            var left = x == null ? y : x;
//            var right = x != null ? y : x;

//            return left.Equals(right);
//        }

//        public static bool operator !=(ChessPlayerMove obj1, ChessPlayerMove obj2)
//        {
//            return !(obj1 == obj2);
//        }

//        public override string ToString()
//        {
//            return $"{Move.ToString()}";
//        }

//        public bool Equals(ChessPlayerMove other)
//        {
//            if (other is null)
//            {
//                return false;
//            }

//            if(ReferenceEquals(this, other))
//            {
//                return true;
//            }

//            return other.Owner == this.Owner
//                    && other.Move == this.Move;
//        }

//        public override bool Equals(object obj)
//        {
//            var other = obj as ChessPlayerMove;
//            return this.Equals(other);
//        }

//        public override int GetHashCode()
//        {
//            unchecked
//            {
//                int hash = Constants.HashBase;

//                hash = (hash ^ Constants.HashXor) ^ this.Owner.GetHashCode();
//                hash = (hash ^ Constants.HashXor) ^ this.Move.GetHashCode();

//                return hash;
//            }
//        }

//        #region Shorthands for creation

//        public static ChessPlayerMove CreatePawnMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new PawnMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreatePawnCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new PawnCaptureMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreatePawnEnPassantCaptureMove(ChessPlayer owner, Position from, Position to, Position capturePosition)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new PawnEnPassantMove { From = from, To = to, CapturePosition = capturePosition }
//            };
//        }

//        public static ChessPlayerMove CreatePawnPromotionalMove(ChessPlayer owner, Position from, Position to, ChessPiece promoteTo)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new PawnPromotionalMove { From = from, To = to, PromoteTo = promoteTo }
//            };
//        }

//        public static ChessPlayerMove CreatePawnPromotionalCaptureMove(ChessPlayer owner, Position from, Position to, ChessPiece promoteTo)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new PawnPromotionalMove { From = from, To = to, PromoteTo = promoteTo }
//            };
//        }

//        public static ChessPlayerMove CreateKingMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KingMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateKingCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KingCaptureMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateKingShortCastlingMove(ChessPlayer owner, Position from, Position to, Position rookPosition)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KingShortCastlingMove { From = from, To = to, RookPosition = rookPosition }
//            };
//        }

//        public static ChessPlayerMove CreateKingLongCastlingMove(ChessPlayer owner, Position from, Position to, Position rookPosition)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KingLongCastlingMove { From = from, To = to, RookPosition = rookPosition }
//            };
//        }

//        public static ChessPlayerMove CreateBishopMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new BishopMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateBishopCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new BishopCaptureMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateRookMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new RookMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateRookCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new RookCaptureMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateQueenMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new QueenMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateQueenCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new QueenCaptureMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateKnightMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KnightMove { From = from, To = to }
//            };
//        }

//        public static ChessPlayerMove CreateKnightCaptureMove(ChessPlayer owner, Position from, Position to)
//        {
//            return new ChessPlayerMove()
//            {
//                Owner = owner,
//                Move = new KnightCaptureMove { From = from, To = to }
//            };
//        }

//        #endregion
//    }
//}
