using BoardGame.Service.Models.Api.ChessGamesControllerModels;
using BoardGame.Service.Models.Data;
using BoardGame.Service.Models.Data.Moves;
using Game.Chess.Moves;
using System;

namespace BoardGame.Service.Models.Converters
{
    /// <summary>
    /// One-way implementor of the DB Chess Game to Chess Game conversion.
    /// </summary>
    public class ChessGameRepositoryConverter : IChessGameRepositoryConverter
    {
        /// <inheritdoc />
        public ChessGame ConvertToChessGame(DbChessGame source)
        {
            if(source == null)
            {
                return null;
            }

            return new ChessGame
            {
                ChallengeDate = source.ChallengeDate,
                Id = source.Id,
                InitiatedBy = ConvertUser(source.InitiatedBy),
                LastMoveDate = source.LastMoveDate,
                Name = source.Name,
                Opponent = ConvertUser(source.Opponent),
                BlackPlayer = ConvertUser(source.BlackPlayer),
                WhitePlayer = ConvertUser(source.WhitePlayer),
            };
        }

        /// <inheritdoc />
        public ChessGameDetails ConvertToChessGameDetails(DbChessGame source)
        {
            if (source == null)
            {
                return null;
            }

            return new ChessGameDetails
            {
                ChallengeDate = source.ChallengeDate,
                Id = source.Id,
                InitiatedBy = ConvertUser(source.InitiatedBy),
                LastMoveDate = source.LastMoveDate,
                Name = source.Name,
                Opponent = ConvertUser(source.Opponent),
                BlackPlayer = ConvertUser(source.BlackPlayer),
                WhitePlayer = ConvertUser(source.WhitePlayer),
                Representation = new Game.Chess.ChessRepresentationInitializer().Create()
            };
        }

        /// <inheritdoc />
        public ChessGamePlayerDto ConvertUser(ApplicationUser source)
        {
            if (source == null)
            {
                return null;
            }

            return new ChessGamePlayerDto
            {
                Id = source.Id,
                UserName = source.UserName
            };
        }

        /// <inheritdoc />
        public ChessMove CovertToChessMove(DbChessMove dbMove)
        {
            if(dbMove == null)
            {
                return null;
            }

            switch (dbMove)
            {
                case DbKingCastlingMove castling:
                    return new KingCastlingMove
                    {
                        //ChessMoveResult = castling.ChessMoveResult,
                        ChessPiece = castling.ChessPiece,
                        From = castling.From,
                        IsCaptureMove = castling.IsCaptureMove,
                        Owner = castling.Owner,
                        To = castling.To,
                        CastlingType = castling.CastlingType
                    };

                case DbPawnPromotionalMove promotionalMove:
                    return new PawnPromotionalMove
                    {
                        //ChessMoveResult = promotionalMove.ChessMoveResult,
                        ChessPiece = promotionalMove.ChessPiece,
                        From = promotionalMove.From,
                        IsCaptureMove = promotionalMove.IsCaptureMove,
                        Owner = promotionalMove.Owner,
                        To = promotionalMove.To,
                        PromoteTo = promotionalMove.PromoteTo
                    };

                case DbPawnEnPassantMove enPassantMove:
                    return new PawnEnPassantMove
                    {
                        //ChessMoveResult = enPassantMove.ChessMoveResult,
                        ChessPiece = enPassantMove.ChessPiece,
                        From = enPassantMove.From,
                        IsCaptureMove = enPassantMove.IsCaptureMove,
                        Owner = enPassantMove.Owner,
                        To = enPassantMove.To,
                        CapturePosition = enPassantMove.CapturePosition
                    };
            }

            return new ChessMove
            {
                //ChessMoveResult = dbMove.ChessMoveResult,
                ChessPiece = dbMove.ChessPiece,
                From = dbMove.From,
                IsCaptureMove = dbMove.IsCaptureMove,
                Owner = dbMove.Owner,
                To = dbMove.To
            };
        }

        /// <inheritdoc />
        public DbChessMove CovertToDbChessMove(ChessMove move)
        {
            if (move == null)
            {
                return null;
            }

            switch (move)
            {
                case KingCastlingMove castling:
                    return new DbKingCastlingMove
                    {
                        //ChessMoveResult = castling.ChessMoveResult,
                        ChessPiece = castling.ChessPiece,
                        IsCaptureMove = castling.IsCaptureMove,
                        Owner = castling.Owner,
                        CastlingType = castling.CastlingType,
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        FromColumn = castling.From.Column.ToString(),
                        FromRow = castling.From.Row,
                        ToColumn = castling.To.Column.ToString(),
                        ToRow = castling.To.Row,
                        MoveId = Guid.NewGuid()
                    };

                case PawnPromotionalMove promotionalMove:
                    return new DbPawnPromotionalMove
                    {
                        //ChessMoveResult = promotionalMove.ChessMoveResult,
                        ChessPiece = promotionalMove.ChessPiece,
                        IsCaptureMove = promotionalMove.IsCaptureMove,
                        Owner = promotionalMove.Owner,
                        PromoteTo = promotionalMove.PromoteTo,
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        FromColumn = promotionalMove.From.Column.ToString(),
                        FromRow = promotionalMove.From.Row,
                        ToColumn = promotionalMove.To.Column.ToString(),
                        ToRow = promotionalMove.To.Row,
                        MoveId = Guid.NewGuid(),
                    };

                case PawnEnPassantMove enPassantMove:
                    return new DbPawnEnPassantMove
                    {
                        //ChessMoveResult = enPassantMove.ChessMoveResult,
                        ChessPiece = enPassantMove.ChessPiece,
                        IsCaptureMove = enPassantMove.IsCaptureMove,
                        Owner = enPassantMove.Owner,
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        FromColumn = enPassantMove.From.Column.ToString(),
                        FromRow = enPassantMove.From.Row,
                        ToColumn = enPassantMove.To.Column.ToString(),
                        ToRow = enPassantMove.To.Row,
                        MoveId = Guid.NewGuid(),
                        CapturePositionColumn = enPassantMove.CapturePosition.Column.ToString(),
                        CapturePositionRow = enPassantMove.CapturePosition.Row
                    };
            }

            return new DbChessMove
            {
                //ChessMoveResult = move.ChessMoveResult,
                ChessPiece = move.ChessPiece,
                IsCaptureMove = move.IsCaptureMove,
                Owner = move.Owner,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                FromColumn = move.From.Column.ToString(),
                FromRow = move.From.Row,
                ToColumn = move.To.Column.ToString(),
                ToRow = move.To.Row,
                MoveId = Guid.NewGuid()
            };
        }
    }
}
