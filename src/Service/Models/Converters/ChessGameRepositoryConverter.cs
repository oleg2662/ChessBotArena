using BoardGame.Service.Models.Data;
using BoardGame.Service.Models.Data.Moves;
using Game.Chess.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Chess;
using Model.Api.ChessGamesControllerModels;

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
                Outcome = source.Status
            };
        }

        /// <inheritdoc />
        public ChessGameDetails ConvertToChessGameDetails(DbChessGame source)
        {
            if (source == null)
            {
                return null;
            }

            var history = source.History?.OrderBy(x => x.CreatedAt).Select(CovertToChessMove).ToList() ?? new List<BaseMove>();

            var representation = new ChessRepresentationInitializer().Create();
            var chessMechanism = new ChessMechanism();
            foreach (var move in history)
            {
                representation = chessMechanism.ApplyMove(representation, move);
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
                Representation = representation
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
        public BaseMove CovertToChessMove(DbBaseMove dbMove)
        {
            if(dbMove == null)
            {
                return null;
            }

            switch (dbMove)
            {
                case DbKingCastlingMove castling:
                    return new KingCastlingMove(castling.Owner, castling.CastlingType);

                case DbPawnPromotionalMove promotionalMove:
                    return new PawnPromotionalMove(promotionalMove.Owner, promotionalMove.From, promotionalMove.To, promotionalMove.PromoteTo);

                case DbPawnEnPassantMove enPassantMove:
                    return new PawnEnPassantMove(enPassantMove.Owner, enPassantMove.From, enPassantMove.To, enPassantMove.CapturePosition);

                case DbChessMove chessMove:
                    return new ChessMove(chessMove.Owner, chessMove.From, chessMove.To);

                case DbSpecialMove specialMove:
                    return new SpecialMove(specialMove.Owner, specialMove.Message);

                default:
                    throw new ArgumentOutOfRangeException(nameof(dbMove));
            }
        }

        /// <inheritdoc />
        public DbBaseMove CovertToDbChessMove(BaseMove move)
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
                        Owner = castling.Owner,
                        CastlingType = castling.CastlingType,
                        CreatedAt = DateTime.UtcNow,
                        FromColumn = castling.From.Column.ToString(),
                        FromRow = castling.From.Row,
                        ToColumn = castling.To.Column.ToString(),
                        ToRow = castling.To.Row,
                        MoveId = Guid.NewGuid()
                    };

                case PawnPromotionalMove promotionalMove:
                    return new DbPawnPromotionalMove
                    {
                        Owner = promotionalMove.Owner,
                        PromoteTo = promotionalMove.PromoteTo,
                        CreatedAt = DateTime.UtcNow,
                        FromColumn = promotionalMove.From.Column.ToString(),
                        FromRow = promotionalMove.From.Row,
                        ToColumn = promotionalMove.To.Column.ToString(),
                        ToRow = promotionalMove.To.Row,
                        MoveId = Guid.NewGuid(),
                    };

                case PawnEnPassantMove enPassantMove:
                    return new DbPawnEnPassantMove
                    {
                        Owner = enPassantMove.Owner,
                        CreatedAt = DateTime.UtcNow,
                        FromColumn = enPassantMove.From.Column.ToString(),
                        FromRow = enPassantMove.From.Row,
                        ToColumn = enPassantMove.To.Column.ToString(),
                        ToRow = enPassantMove.To.Row,
                        MoveId = Guid.NewGuid(),
                        CapturePositionColumn = enPassantMove.CapturePosition.Column.ToString(),
                        CapturePositionRow = enPassantMove.CapturePosition.Row
                    };

                case BaseChessMove chessMove:
                    return new DbChessMove
                    {
                        Owner = chessMove.Owner,
                        CreatedAt = DateTime.UtcNow,
                        FromColumn = chessMove.From.Column.ToString(),
                        FromRow = chessMove.From.Row,
                        ToColumn = chessMove.To.Column.ToString(),
                        ToRow = chessMove.To.Row,
                        MoveId = Guid.NewGuid()
                    };

                case SpecialMove specialMove:
                    return new DbSpecialMove
                    {
                        Owner = specialMove.Owner,
                        Message = specialMove.Message,
                        CreatedAt = DateTime.UtcNow,
                        MoveId = Guid.NewGuid()
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(move));
            }
        }
    }
}
