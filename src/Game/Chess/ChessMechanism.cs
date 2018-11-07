using Game.Chess.Exceptions;

namespace Game.Chess
{
    using Abstraction;
    using Moves;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Pieces;

    public class ChessMechanism : IMechanism<ChessRepresentation, BaseMove, GameState>
    {
        //private readonly Dictionary<ChessMechanismCacheKey, IEnumerable<BaseChessMove>> _moveCache = new Dictionary<ChessMechanismCacheKey, IEnumerable<BaseChessMove>>();

        public IEnumerable<BaseMove> GenerateMoves(ChessRepresentation representation)
        {
            var originalBoard = representation;
            var currentPlayer = representation.CurrentPlayer;

            var specialMovesInHistory = originalBoard.History.OfType<SpecialMove>().ToList();
            if (specialMovesInHistory.Any(x => x.Message == MessageType.Resign))
            {
                yield break;
            }

            yield return new SpecialMove(currentPlayer, MessageType.Resign);

            if (specialMovesInHistory.Any(x => x.Message == MessageType.DrawOffer))
            {
                yield return new SpecialMove(currentPlayer, MessageType.DrawAccept);
                yield return new SpecialMove(currentPlayer, MessageType.DrawDecline);
                yield break;
            }
            else
            {
                yield return new SpecialMove(currentPlayer, MessageType.DrawOffer);
            }

            var possibleMoves = GenerateMoves(representation, representation.CurrentPlayer);

            foreach (var move in possibleMoves)
            {
                var newRepresentation = ApplyMove(representation, move, false);
                var movingPiece = originalBoard[move.From];
                IEnumerable<Position> threatenedPositions;
                var originalKingPosition = FindKing(newRepresentation, currentPlayer);

                if (movingPiece.Kind != PieceKind.King || !(move is KingCastlingMove))
                {
                    // Check whether the move would threaten the king
                    threatenedPositions = GetThreatenedPositions(newRepresentation, currentPlayer);
                    if(!threatenedPositions.Contains(originalKingPosition))
                    {
                        yield return move;
                    }
                }
                else
                {
                    // Check castling requirements...
                    threatenedPositions = GetThreatenedPositions(representation, currentPlayer);
                    var castlingMove = (KingCastlingMove)move;

                    switch (castlingMove.CastlingType)
                    {
                        case CastlingType.Long:
                            var longCastlingPositions = currentPlayer == ChessPlayer.White
                                ? new[] { Positions.B1, Positions.C1, Positions.D1 }
                                : new[] { Positions.B8, Positions.C8, Positions.D8 };

                            if (threatenedPositions.Intersect(longCastlingPositions).Any())
                            {
                                continue;
                            }

                            yield return move;
                            break;

                        case CastlingType.Short:
                            var shortCastlingPositions = currentPlayer == ChessPlayer.White
                                ? new[] { Positions.F1, Positions.G1 }
                                : new[] { Positions.F8, Positions.G8 };

                            if (threatenedPositions.Intersect(shortCastlingPositions).Any())
                            {
                                continue;
                            }

                            yield return move;
                            break;
                    }
                }
            }
        }

        public GameState GetGameState(ChessRepresentation representation)
        {
            var specialMoves = new HashSet<SpecialMove>(representation.History.OfType<SpecialMove>());

            // Search for resign.
            var resign = specialMoves.FirstOrDefault(x => x.Message == MessageType.Resign);

            switch (resign?.Owner)
            {
                case ChessPlayer.White:
                    return GameState.BlackWon;
                case ChessPlayer.Black:
                    return GameState.WhiteWon;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Search for draw accept
            if (specialMoves.Any(x => x.Message == MessageType.DrawAccept))
            {
                return GameState.Draw;
            }

            // Check check-mate
            var isCurrentPlayerInChess = IsPlayerInChess(representation, representation.CurrentPlayer);

            var anyMovesLeft = GenerateMoves(representation).Any();

            if (!anyMovesLeft && isCurrentPlayerInChess)
            {
                // Check-mate!
                switch (representation.CurrentPlayer)
                {
                    case ChessPlayer.White:
                        return GameState.BlackWon;
                    case ChessPlayer.Black:
                        return GameState.WhiteWon;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // Check stale-mate
            if (!anyMovesLeft)
            {
                return GameState.Draw;
            }

            var nonEmptyPositions = Positions.PositionList.Select(x => new
                                    {
                                        ChessPiece = representation[x],
                                        Position = x
                                    }).Where(x => x.ChessPiece != null)
                                      .ToArray();


            // King vs king --> Draw
            if (nonEmptyPositions.Length == 2)
            {
                return GameState.Draw;
            }

            // King vs (king and (bishop or knight)
            if (nonEmptyPositions.Length == 3 && nonEmptyPositions.Any(x =>
                    x.ChessPiece.Kind == PieceKind.Bishop || x.ChessPiece.Kind == PieceKind.Knight))
            {
                return GameState.Draw;
            }

            // King and bishop vs King vs bishop (bishops on same coloured field.)
            if (nonEmptyPositions.Length == 4
                && nonEmptyPositions.Count(x => x.ChessPiece.Owner == ChessPlayer.Black) == 2
                && nonEmptyPositions.Count(x => x.ChessPiece.Kind == PieceKind.Bishop) == 2
                && nonEmptyPositions.Where(x => x.ChessPiece.Kind == PieceKind.Bishop)
                                    .Select(x => x.Position.BlackField)
                                    .Distinct()
                                    .Count() == 1)
            {
                return GameState.Draw;
            }

            return GameState.InProgress;
        }

        private bool IsPlayerInChess(ChessRepresentation representation, ChessPlayer player)
        {
            var kingPosition = FindKing(representation, player);
            var threatenedPositions = GetThreatenedPositions(representation, player);

            return threatenedPositions.Contains(kingPosition);
        }

        public bool ValidateMove(ChessRepresentation representation, BaseMove move)
        {
            return GenerateMoves(representation).Contains(move);
        }

        public ChessRepresentation ApplyMove(ChessRepresentation representationParam, BaseMove move)
        {
            return ApplyMove(representationParam, move, true);
        }

        private ChessRepresentation ApplyMove(ChessRepresentation representationParam, BaseMove move, bool validateMove)
        {
            if (validateMove && !ValidateMove(representationParam, move))
            {
                throw new ChessIllegalMoveException();
            }

            var representation = representationParam.Clone();

            // Removing en passant flags
            var pawns = Positions.PositionList.Select(x => representation[x])
                .Where(x => x != null)
                .Where(x => x.Owner != representation.CurrentPlayer)
                .OfType<Pawn>()
                .Select(x => x)
                .ToArray();

            foreach (var pawn in pawns)
            {
                pawn.IsEnPassantCapturable = false;
            }

            switch (move)
            {
                case SpecialMove _:
                    break;

                case KingCastlingMove castlingMove:
                    representation.Move(castlingMove.From, castlingMove.To);
                    representation.Move(castlingMove.RookFrom, castlingMove.RookTo);
                    representation[castlingMove.To].HasMoved = true;
                    representation[castlingMove.RookTo].HasMoved = true;
                    break;

                case PawnEnPassantMove pawnEnPassantMove:
                    representation.Move(pawnEnPassantMove.From, pawnEnPassantMove.To);
                    representation[pawnEnPassantMove.CapturePosition] = null;
                    representation[pawnEnPassantMove.To].HasMoved = true;
                    break;

                case PawnPromotionalMove pawnPromotionalMove:
                    representation.Move(pawnPromotionalMove.From, pawnPromotionalMove.To);
                    representation[pawnPromotionalMove.To] = ChessPieces.Create(pawnPromotionalMove.PromoteTo, representation[pawnPromotionalMove.To].Owner);
                    representation[pawnPromotionalMove.To].HasMoved = true;
                    break;

                case BaseChessMove chessMove:
                    var movingPiece = representation[chessMove.From];
                    representation.Move(chessMove.From, chessMove.To);
                    representation[chessMove.To].HasMoved = true;
                    
                    // Setting en passant flag if needed
                    if (movingPiece.Kind == PieceKind.Pawn && Math.Abs(chessMove.From.Row - chessMove.To.Row) == 2 && chessMove.From.Column == chessMove.To.Column)
                    {
                        ((Pawn)representation[chessMove.To]).IsEnPassantCapturable = true;
                    }
                    break;
            }

            representation.TogglePlayer();
            representation.History.Add(move);

            return representation;
        }

        private Position FindKing(ChessRepresentation representation, ChessPlayer player)
        {
            var position = Positions.PositionList
                                    .Where(x => representation[x] != null)
                                    .Where(x => representation[x].Owner == player)
                                    .FirstOrDefault(x => representation[x].Kind == PieceKind.King);

            return position;
        }

        private ChessPlayer GetOpponent(ChessPlayer player)
        {
            switch (player)
            {
                case ChessPlayer.Black: return ChessPlayer.White;
                case ChessPlayer.White: return ChessPlayer.Black;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<Position> GetThreatenedPositions(ChessRepresentation board, ChessPlayer threatenedPlayer)
        {
            var threateningPlayer = GetOpponent(threatenedPlayer);
            var opponentMoves = new List<BaseChessMove>();

            //var key = new ChessMechanismCacheKey(board, threateningPlayer, ChessMechanismCacheKey.MoveGeneratorStyle.Threatening);
            //if (_moveCache.ContainsKey(key))
            //{
            //    opponentMoves = _moveCache[key].ToList();
            //}
            //else
            //{

                foreach (var from in Positions.PositionList)
                {
                    var piece = board[from];

                    if (piece == null || piece.Owner == threatenedPlayer)
                    {
                        continue;
                    }

                    switch (piece.Kind)
                    {
                        case PieceKind.King:
                            opponentMoves.AddRange(GetKingMoves(board, from, threateningPlayer));
                            break;

                        case PieceKind.Queen:
                            opponentMoves.AddRange(GetQueenMoves(board, from, threateningPlayer));
                            break;

                        case PieceKind.Rook:
                            opponentMoves.AddRange(GetRookMoves(board, from, threateningPlayer));
                            break;

                        case PieceKind.Bishop:
                            opponentMoves.AddRange(GetBishopMoves(board, from, threateningPlayer));
                            break;

                        case PieceKind.Knight:
                            opponentMoves.AddRange(GetKnightMoves(board, from, threateningPlayer));
                            break;

                        case PieceKind.Pawn:
                            opponentMoves.AddRange(GetPawnMoves(board, from, threateningPlayer, true));
                            break;

                        default:
                            continue;
                    }
                }
            //}

            var result = opponentMoves.Select(x => x.To).Distinct().ToList();

            return result;
        }

        private IEnumerable<BaseChessMove> GenerateMoves(ChessRepresentation representation, ChessPlayer player)
        {
            //var key = new ChessMechanismCacheKey(representation, player, ChessMechanismCacheKey.MoveGeneratorStyle.Normal);
            //if (_moveCache.ContainsKey(key))
            //{
            //    return _moveCache[key];
            //}

            var possibleMoves = Positions.PositionList
                .Where(x => representation[x] != null && representation[x].Owner == representation.CurrentPlayer)
                .SelectMany(x => GetChessMoves(representation, x, player))
                .ToList();

            //_moveCache.Add(key, possibleMoves);

            return possibleMoves;
        }

        private IEnumerable<BaseChessMove> GetChessMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            var piece = board[from];

            if (piece == null || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            switch (piece.Kind)
            {
                case PieceKind.King: return GetKingMoves(board, from, player).Union(GetCastlings(board, player));
                case PieceKind.Queen: return GetQueenMoves(board, from, player);
                case PieceKind.Rook: return GetRookMoves(board, from, player);
                case PieceKind.Bishop: return GetBishopMoves(board, from, player);
                case PieceKind.Knight: return GetKnightMoves(board, from, player);
                case PieceKind.Pawn: return GetPawnMoves(board, from, player, false);
                default: return Enumerable.Empty<BaseChessMove>();
            }
        }

        private IEnumerable<BaseChessMove> GetPawnMoves(ChessRepresentation board, Position from, ChessPlayer player, bool onlyThreateningMoves)
        {
            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Pawn || piece.Owner != player)
            {
                yield break;
            }

            Position stepForward;
            Position enPassantEastPosition;
            Position enPassantWestPosition;
            Position doubleStepForwardOpening;
            Position captureEast;
            Position captureWest;

            switch (piece.Owner)
            {
                case ChessPlayer.White:
                    stepForward = !onlyThreateningMoves ? from.North() : null;
                    enPassantEastPosition = from.East();
                    enPassantWestPosition = from.West();
                    captureEast = from.NorthEast();
                    captureWest = from.NorthWest();
                    doubleStepForwardOpening = piece.HasMoved || onlyThreateningMoves
                                                ? null
                                                : board[stepForward] == null
                                                    ? from.North(2)
                                                    : null;

                    if (stepForward != null && board[stepForward] == null)
                    {
                        if (stepForward.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, stepForward);
                        }
                    }

                    if (doubleStepForwardOpening != null && board[doubleStepForwardOpening] == null)
                    {
                        yield return new ChessMove(player, from, doubleStepForwardOpening);
                    }

                    if (captureEast != null && board[captureEast] != null && board[captureEast].Owner != player)
                    {
                        if (captureEast.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, captureEast);
                        }
                    }

                    if (captureWest != null && board[captureWest] != null && board[captureWest].Owner != player)
                    {
                        if (captureWest.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, captureWest);
                        }
                    }

                    if (enPassantEastPosition != null
                        && captureEast != null
                        && board[captureEast] == null
                        && board[enPassantEastPosition] != null
                        && board[enPassantEastPosition].Owner != player
                        && board[enPassantEastPosition].Kind == PieceKind.Pawn
                        && ((board[enPassantEastPosition] as Pawn)?.IsEnPassantCapturable ?? false))
                    {
                        yield return new PawnEnPassantMove(player, from, captureEast, enPassantEastPosition);
                    }

                    if (enPassantWestPosition != null
                        && captureWest != null
                        && board[captureWest] == null // This likely won't be the case
                        && board[enPassantWestPosition] != null
                        && board[enPassantWestPosition].Owner != player
                        && board[enPassantWestPosition].Kind == PieceKind.Pawn
                        && ((board[enPassantWestPosition] as Pawn)?.IsEnPassantCapturable ?? false))
                    {
                        yield return new PawnEnPassantMove(player, from, captureWest, enPassantWestPosition);
                    }
                    break;

                case ChessPlayer.Black:
                    stepForward = !onlyThreateningMoves ? from.South() : null;
                    enPassantEastPosition = from.East();
                    enPassantWestPosition = from.West();
                    captureEast = from.NorthEast();
                    captureWest = from.NorthWest();
                    doubleStepForwardOpening = piece.HasMoved || onlyThreateningMoves
                                                    ? null
                                                    : board[stepForward] == null
                                                        ? from.South(2)
                                                        : null;

                    if (stepForward != null && board[stepForward] == null)
                    {
                        if (stepForward.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, stepForward, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, stepForward);
                        }
                    }

                    if (doubleStepForwardOpening != null && board[doubleStepForwardOpening] == null)
                    {
                        yield return new ChessMove(player, from, doubleStepForwardOpening);
                    }

                    if (captureEast != null && board[captureEast] != null && board[captureEast].Owner != player)
                    {
                        if (captureEast.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, captureEast, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, captureEast);
                        }
                    }

                    if (captureWest != null && board[captureWest] != null && board[captureWest].Owner != player)
                    {
                        if (captureWest.Row == 1)
                        {
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Bishop);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Knight);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Queen);
                            yield return new PawnPromotionalMove(player, from, captureWest, PieceKind.Rook);
                        }
                        else
                        {
                            yield return new ChessMove(player, from, captureWest);
                        }
                    }

                    if (enPassantEastPosition != null
                        && captureEast != null
                        && board[captureEast] == null // This likely won't be the case
                        && board[enPassantEastPosition] != null
                        && board[enPassantEastPosition].Owner != player
                        && board[enPassantEastPosition].Kind == PieceKind.Pawn
                        && ((board[enPassantEastPosition] as Pawn)?.IsEnPassantCapturable ?? false))
                    {
                        yield return new PawnEnPassantMove(player, from, captureEast, enPassantEastPosition);
                    }

                    if (enPassantWestPosition != null
                        && captureWest != null
                        && board[captureWest] == null // This likely won't be the case
                        && board[enPassantWestPosition] != null
                        && board[enPassantWestPosition].Owner != player
                        && board[enPassantWestPosition].Kind == PieceKind.Pawn
                        && ((board[enPassantWestPosition] as Pawn)?.IsEnPassantCapturable ?? false))
                    {
                        yield return new PawnEnPassantMove(player, from, captureWest, enPassantWestPosition);
                    }
                    break;

                default:
                    yield break;
            }
        }

        private IEnumerable<BaseChessMove> GetKingMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            if (board == null)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.King || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var moves = from.AllDirectionsMove(1)
                .Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove(player, from, x));

            return moves;
        }

        private IEnumerable<BaseChessMove> GetCastlings(ChessRepresentation board, ChessPlayer player)
        {
            var from = player == ChessPlayer.White ? Positions.E1 : Positions.E8;

            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.King || piece.Owner != player)
            {
                yield break;
            }

            if(piece.HasMoved)
            {
                yield break;
            }

            // Long castling trivial possibility check...
            var longCastlingEmptyPositions = player == ChessPlayer.White
                ? new[] { Positions.B1, Positions.C1, Positions.D1 }
                : new[] { Positions.B8, Positions.C8, Positions.D8 };

            var longCastlingRookPosition = player == ChessPlayer.White
                ? Positions.A1
                : Positions.A8;

            var longCastlingNoThreatPositions = player == ChessPlayer.White
                ? new[] { Positions.C1, Positions.D1, Positions.E1 }
                : new[] { Positions.C8, Positions.D8, Positions.E8 };

            var longCastlingEmpty = longCastlingEmptyPositions
                                        .Select(x => board[x])
                                        .Count(x => x == null) == longCastlingEmptyPositions.Count();

            var longCastlingSeemsPossible = board[longCastlingRookPosition] != null
                                             && board[longCastlingRookPosition]?.Kind == PieceKind.Rook
                                             && !board[longCastlingRookPosition].HasMoved
                                             && longCastlingEmpty;

            // Short castling trivial possibility check...
            var shortCastlingEmptyPositions = player == ChessPlayer.White
                ? new[] { Positions.F1, Positions.G1 }
                : new[] { Positions.F8, Positions.G8 };

            var shortCastlingRookPosition = player == ChessPlayer.White
                ? Positions.H1
                : Positions.H8;

            var shortCastlingNoThreatPositions = player == ChessPlayer.White
                ? new[] { Positions.E1, Positions.F1, Positions.G1 }
                : new[] { Positions.E8, Positions.F8, Positions.G8 };

            var shortCastlingEmpty = shortCastlingEmptyPositions
                                        .Select(x => board[x])
                                        .Count(x => x == null) == shortCastlingEmptyPositions.Count();

            var shortCastlingSeemsPossible = board[shortCastlingRookPosition] != null
                                              && board[shortCastlingRookPosition]?.Kind == PieceKind.Rook
                                              && !board[shortCastlingRookPosition].HasMoved
                                              && shortCastlingEmpty;


            var anyCastlingPossible = shortCastlingSeemsPossible || longCastlingSeemsPossible;

            if(!anyCastlingPossible)
            {
                yield break;
            }

            var threatenedPositions = GetThreatenedPositions(board, player).ToList();

            if(longCastlingSeemsPossible && !threatenedPositions.Intersect(longCastlingNoThreatPositions).Any())
            {
                yield return new KingCastlingMove(player, CastlingType.Long);
            }

            if (shortCastlingSeemsPossible && !threatenedPositions.Intersect(shortCastlingNoThreatPositions).Any())
            {
                yield return new KingCastlingMove(player, CastlingType.Short);
            }
        }

        private IEnumerable<BaseChessMove> GetBishopMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Bishop || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(board, from, x => x.NorthEast()));
            positions.AddRange(PositionIterate(board, from, x => x.NorthWest()));
            positions.AddRange(PositionIterate(board, from, x => x.SouthEast()));
            positions.AddRange(PositionIterate(board, from, x => x.SouthWest()));

            var moves = positions.Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove(player, from, x));

            return moves;
        }

        private IEnumerable<BaseChessMove> GetQueenMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Queen || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(board, from, x => x.North()));
            positions.AddRange(PositionIterate(board, from, x => x.South()));
            positions.AddRange(PositionIterate(board, from, x => x.East()));
            positions.AddRange(PositionIterate(board, from, x => x.West()));
            positions.AddRange(PositionIterate(board, from, x => x.NorthEast()));
            positions.AddRange(PositionIterate(board, from, x => x.NorthWest()));
            positions.AddRange(PositionIterate(board, from, x => x.SouthEast()));
            positions.AddRange(PositionIterate(board, from, x => x.SouthWest()));

            return positions.Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove(player, from, x));
        }

        private IEnumerable<BaseChessMove> GetRookMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Rook || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(board, from, x => x.North()));
            positions.AddRange(PositionIterate(board, from, x => x.South()));
            positions.AddRange(PositionIterate(board, from, x => x.East()));
            positions.AddRange(PositionIterate(board, from, x => x.West()));

            return positions.Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove(player, from, x));
        }

        private IEnumerable<BaseChessMove> GetKnightMoves(ChessRepresentation board, Position from, ChessPlayer player)
        {
            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Knight || piece.Owner != player)
            {
                return Enumerable.Empty<BaseChessMove>();
            }

            var knightMoves = from.KnightMoves();

            return knightMoves.Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove(player, from, x));
        }

        private IEnumerable<Position> PositionIterate(ChessRepresentation board, Position from, Func<Position, Position> positionModifier)
        {
            var piece = board[from];

            if (piece == null)
            {
                yield break;
            }

            var player = piece.Owner;

            var step = positionModifier(from);

            while (true)
            {
                if (step == null)
                {
                    break;
                }

                var p = board[step];

                if (p == null)
                {
                    yield return step;
                    step = positionModifier(step);
                    continue;
                }

                if (p.Owner == player)
                {
                    break;
                }
                else
                {
                    yield return step;
                    break;
                }
            }
        }

        //private class ChessMechanismCacheKey : IEquatable<ChessMechanismCacheKey>
        //{
        //    public bool Equals(ChessMechanismCacheKey other)
        //    {
        //        if (ReferenceEquals(null, other)) return false;
        //        if (ReferenceEquals(this, other)) return true;

        //        return ChessRepresentation.Equals(other.ChessRepresentation)
        //               && Player.Equals(other.Player)
        //               && Style.Equals(other.Style);
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        if (ReferenceEquals(null, obj)) return false;
        //        if (ReferenceEquals(this, obj)) return true;
        //        if (obj.GetType() != GetType()) return false;

        //        return Equals((ChessMechanismCacheKey) obj);
        //    }

        //    public override int GetHashCode()
        //    {
        //        unchecked
        //        {
        //            return ((ChessRepresentation != null ? ChessRepresentation.GetHashCode() : 0) * 397) ^ (int) Player ^ (int) Style;
        //        }
        //    }

        //    public static bool operator ==(ChessMechanismCacheKey left, ChessMechanismCacheKey right)
        //    {
        //        return Equals(left, right);
        //    }

        //    public static bool operator !=(ChessMechanismCacheKey left, ChessMechanismCacheKey right)
        //    {
        //        return !Equals(left, right);
        //    }

        //    public ChessRepresentation ChessRepresentation { get; }
        //    public ChessPlayer Player { get; }
        //    public MoveGeneratorStyle Style { get; }

        //    public ChessMechanismCacheKey(ChessRepresentation chessRepresentation, ChessPlayer chessPlayer, MoveGeneratorStyle style)
        //    {
        //        ChessRepresentation = chessRepresentation;
        //        Player = chessPlayer;
        //        Style = style;
        //    }

        //    public enum MoveGeneratorStyle
        //    {
        //        Normal,
        //        Threatening
        //    }
        //}
    }
}
