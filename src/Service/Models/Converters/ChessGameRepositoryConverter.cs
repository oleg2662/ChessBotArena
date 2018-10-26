using BoardGame.Service.Models.Api.ChessGamesControllerModels;
using BoardGame.Service.Models.Data;

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
                //Foo = "custom bar"
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
    }
}
