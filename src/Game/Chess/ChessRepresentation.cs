using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Game.Abstraction;
using Game.Chess.Moves;
using Game.Chess.Pieces;

namespace Game.Chess
{
    [Serializable]
    public sealed class ChessRepresentation : ICloneable<ChessRepresentation>, IEquatable<ChessRepresentation>
    {
        public bool Equals(ChessRepresentation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return _pieces.SequenceEqual(other._pieces)
                   && CurrentPlayer.Equals(other.CurrentPlayer)
                   && History.SequenceEqual(other.History);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ChessRepresentation other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var historyHash = 0;
                foreach (var baseMove in History)
                {
                    historyHash += baseMove.GetHashCode();
                }

                var piecesHashCode = 0;
                for(var i = 0; i < 64; i++)
                {
                    var pieceHashcode = _pieces[i]?.GetHashCode() ?? 0;
                    var positionHashCode = ((Position) i).GetHashCode();
                    piecesHashCode += pieceHashcode + positionHashCode;
                }

                var hashCode = Constants.HashBase;
                hashCode = (hashCode * Constants.HashXor) ^ piecesHashCode;
                //hashCode = (hashCode * Constants.HashXor) ^ (Players != null ? Players.GetHashCode() : 0);
                hashCode = (hashCode * Constants.HashXor) ^ CurrentPlayer.GetHashCode();
                hashCode = (hashCode * Constants.HashXor) ^ historyHash;
                return hashCode;
            }
        }

        public static bool operator ==(ChessRepresentation left, ChessRepresentation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ChessRepresentation left, ChessRepresentation right)
        {
            return !Equals(left, right);
        }

        private readonly ChessPiece[] _pieces = new ChessPiece[64];

        public ChessRepresentation()
        {
            History = new List<BaseMove>();
            CurrentPlayer = ChessPlayer.White;
        }

        /// <summary>
        /// Algebraic notation accessor.
        /// </summary>
        /// <param name="position">The position of the field in the board.</param>
        /// <returns>The chess piece on the given field. Returns a null chess piece object if empty. Throws exception if column or row id isn't valid.</returns>
        public ChessPiece this[Position position]
        {
            get
            {
                var idx = (int)position;
                return _pieces[idx];
            }
            set
            {
                var idx = (int)position;
                _pieces[idx] = value;
            }
        }

        /// <summary>
        /// Algebraic notation accessor.
        /// </summary>
        /// <param name="col">Character of the column. Can be A..H.</param>
        /// <param name="row">The row number. Can be between 1..8.</param>
        /// <returns>The chess piece on the given field. Returns a null chess piece object if empty. Throws exception if column or row id isn't valid.</returns>
        public ChessPiece this[char col, int row]
        {
            get
            {
                var idx = new Position(col, row);
                return this[idx];
            }
            set
            {
                var idx = new Position(col, row);
                this[idx] = value;
            }
        }

        /// <summary>
        /// Algebraic notation (string) accessor.
        /// </summary>
        /// <param name="algebraicNotation">Algebraic notation of the position. Example: "B3".</param>
        /// <returns>The chess piece on the given field. Returns a null chess piece object if empty. Throws exception if column or row id isn't valid.</returns>
        public ChessPiece this[string algebraicNotation]
        {
            get
            {
                var idx = (Position) algebraicNotation;
                return this[idx];
            }
            set
            {
                var idx = (Position)algebraicNotation;
                this[idx] = value;
            }
        }

        /// <summary>
        /// Gets or sets the current player.
        /// </summary>
        public ChessPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Gets or sets the players playing the game.
        /// </summary>
        public IEnumerable<ChessPlayer> Players => new[] { ChessPlayer.White, ChessPlayer.Black };

        /// <summary>
        /// Gets the history of the game.
        /// </summary>
        public List<BaseMove> History { get; set; }

        public ChessRepresentation Clone()
        {
            var newBoard = new ChessRepresentation()
            {
                CurrentPlayer = CurrentPlayer,
                History = History.Select(x => x.Clone()).ToList()
            };

            foreach(var p in Positions.PositionList)
            {
                var piece = this[p];
                newBoard[p] = piece?.Clone();
            }

            return newBoard;
        }

        public ChessPiece Move(Position from, Position to)
        {
            var result = this[to];

            this[to] = this[from];
            this[from] = null;

            return result;
        }

        public void TogglePlayer()
        {
            CurrentPlayer = CurrentPlayer == ChessPlayer.Black 
                            ? ChessPlayer.White
                            : ChessPlayer.Black;
        }
    }
}
