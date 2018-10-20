namespace Game.Chess
{
    using Game.Abstraction;
    using Game.Chess.Moves;
    using Game.Chess.Pieces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class ChessBoard : IChessBoard, ICloneable<ChessBoard>
    {
        private ChessPiece[] pieces = new ChessPiece[64];

        public ChessBoard()
        {
            History = new List<ChessMove>();
        }

        /// <summary>
        /// Algebraic notation accessor.
        /// </summary>
        /// <param name="col">Character of the column. Can be A..H.</param>
        /// <param name="row">The row number. Can be between 1..8.</param>
        /// <returns>The chess piece on the given field. Returns a null chess piece object if empty. Throws exception if column or row id isn't valid.</returns>
        public ChessPiece this[Position position]
        {
            get
            {

                var idx = (int)position;
                return pieces[idx];
            }
            set
            {
                var idx = (int)position;
                pieces[idx] = value;
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
        public List<ChessMove> History { get; set; }

        public ChessBoard Clone()
        {
            var newBoard = new ChessBoard()
            {
                CurrentPlayer = this.CurrentPlayer,
                History = this.History.Select(x => x.Clone()).ToList(),
                Players = this.Players.Select(x => x),
            };

            foreach(var p in Positions.PositionList)
            {
                var piece = this[p];
                newBoard[p] = piece == null ? null : piece.Clone();
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
    }
}
