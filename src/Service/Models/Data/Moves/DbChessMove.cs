using Game.Chess;
using Game.Chess.Moves;
using Game.Chess.Pieces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGame.Service.Models.Data.Moves
{
    [Serializable]
    [Table("DbChessMove")]
    public class DbChessMove
    {
        [Key]
        public Guid MoveId { get; set; }

        public int FromRow { get; set; }

        public string FromColumn { get; set; }

        public Position From => new Position(FromColumn[0], FromRow);

        public int ToRow { get; set; }

        public string ToColumn { get; set; }

        public virtual Position To => new Position(ToColumn[0], ToRow);

        public bool IsCaptureMove { get; set; }

        public ChessPlayer Owner { get; set; }

        public PieceKind ChessPiece { get; set; }

        public ChessMoveResult ChessMoveResult { get; set; }
    }
}
