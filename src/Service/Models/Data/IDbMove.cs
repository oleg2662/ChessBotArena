//using Game.Chess;
//using Game.Chess.Moves;
//using Game.Chess.Pieces;
//using System;
//using CastlingTypeEnum = Game.Chess.Moves.CastlingType;

//namespace BoardGame.Service.Models.Data
//{
//    /// <summary>
//    /// Moves representation in the database
//    /// </summary>
//    public interface IDbMove
//    {
//        /// <summary>
//        /// Gets or sets the ID of the move.
//        /// </summary>
//        Guid MoveId { get; set; }

//        /// <summary>
//        /// Gets or sets the start position of the main piece in the move.
//        /// </summary>
//        Position From { get; set; }

//        /// <summary>
//        /// Gets or sets the destination position of the main piece in the move.
//        /// </summary>
//        Position To { get; set; }

//        /// <summary>
//        /// Gets or sets a value indicating whether the move was a capture move.
//        /// </summary>
//        bool IsCaptureMove { get; set; }

//        /// <summary>
//        /// Gets the owner of the moving piece.
//        /// </summary>
//        ChessPlayer Owner { get; set; }

//        /// <summary>
//        /// Gets or sets the moved chess piece.
//        /// </summary>
//        PieceKind ChessPiece { get; set; }

//        /// <summary>
//        /// Gets or sets the result of a move. Example: check.
//        /// </summary>
//        ChessMoveResult ChessMoveResult { get; set; }

//        /// <summary>
//        /// Gets or sets the value indicating the pawn promotion. (If applicable)
//        /// </summary>
//        PieceKind PromoteTo { get; set; }

//        /// <summary>
//        /// Gets or sets the value indicating the en passant capture position. (If applicable)
//        /// </summary>
//        Position CapturePosition { get; set; }

//        /// <summary>
//        /// Gets the start position of the rook participating in a castling. (If applicable)
//        /// </summary>
//        Position RookFrom { get; }

//        /// <summary>
//        /// Gets the destination position of the rook participating in a castling. (If applicable)
//        /// </summary>
//        Position RookTo { get; }

//        /// <summary>
//        /// Gets or sets the type of the castling. (If applicable)
//        /// </summary>
//        CastlingTypeEnum? CastlingType { get; set; }

//        /// <summary>
//        /// Gets or sets the creation date of the move in UTC.
//        /// </summary>
//        DateTime CreatedDate { get; set; }
//    }
//}
