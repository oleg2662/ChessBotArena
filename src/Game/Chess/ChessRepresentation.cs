namespace Game.Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abstraction;
    using Moves;
    using Pieces;

    [Serializable]
    public sealed class ChessRepresentation : ICloneable<ChessRepresentation>
    {
        private readonly ChessPiece[] _pieces = new ChessPiece[64];

        public ChessRepresentation()
        {
            History = new List<BaseMove>();
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
        public IEnumerable<ChessPlayer> Players { get; set; }

        /// <summary>
        /// Gets the history of the game.
        /// </summary>
        public List<BaseMove> History { get; set; }

        public ChessRepresentation Clone()
        {
            var newBoard = new ChessRepresentation()
            {
                CurrentPlayer = CurrentPlayer,
                History = History.Select(x => x.Clone()).ToList(),
                Players = Players.Select(x => x).ToList()
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
