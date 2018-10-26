namespace Game.Chess
{
    using Game.Abstraction;
    using Game.Chess.Moves;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Game.Chess.Extensions;
    using Game.Chess.Pieces;

    public class ChessGame : IMechanism<ChessBoard, ChessMove, GameState>
    {
        public IEnumerable<ChessMove> GenerateMoves(ChessBoard representation)
        {
            var possibleMoves = GenerateMoves(representation, null);
            var originalBoard = representation;
            var currentPlayer = representation.CurrentPlayer;
            var opponent = currentPlayer == ChessPlayer.Black ? ChessPlayer.White : ChessPlayer.Black;

            foreach (var move in possibleMoves)
            {
                var newRepresentation = ApplyMove(representation, move);
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
                    continue;
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

        public GameState GetGameState(ChessBoard representation)
        {
            // TODO : implement!
            return GameState.InProgress;
        }

        public bool ValidateMove(ChessBoard representation, ChessMove move)
        {
            return this.GenerateMoves(representation).Contains(move);
        }

        public ChessBoard ApplyMove(ChessBoard representationParam, ChessMove move)
        {
            var representation = representationParam.Clone();

            switch (move)
            {
                case KingCastlingMove castlingMove:
                    representation.Move(castlingMove.From, castlingMove.To);
                    representation.Move(castlingMove.RookFrom, castlingMove.RookTo);
                    break;
                case PawnEnPassantMove pawnEnPassantMove:
                    representation.Move(pawnEnPassantMove.From, pawnEnPassantMove.To);
                    representation[pawnEnPassantMove.CapturePosition] = null;
                    break;
                case PawnPromotionalMove pawnPromotionalMove:
                    representation.Move(pawnPromotionalMove.From, pawnPromotionalMove.To);
                    representation[pawnPromotionalMove.To] = pawnPromotionalMove.PromoteTo;
                    break;
                default:
                    representation.Move(move.From, move.To);
                    break;
            }

            // Setting moved piece's 'HasMoved' flag
            representation[move.To].HasMoved = true;

            // Removing en passant flag
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

            representation.CurrentPlayer = representation.CurrentPlayer == ChessPlayer.White
                                            ? ChessPlayer.Black
                                            : ChessPlayer.White;

            return representation;
        }

        private Position FindKing(ChessBoard representation, ChessPlayer? player)
        {
            if(representation == null)
            {
                return null;
            }

            player = player ?? representation.CurrentPlayer;

            var position = Positions.PositionList.Where(x => representation[x] != null)
                                    .Where(x => representation[x].Owner == player)
                                    .Where(x => representation[x].Kind == PieceKind.King)
                                    .FirstOrDefault();

            // TODO : If no king, then throw validation check exception!
            return position;
        }

        private IEnumerable<Position> GetThreatenedPositions(ChessBoard representation, ChessPlayer threatenedPlayer)
        {
            var board = representation;

            if(board == null)
            {
                return Enumerable.Empty<Position>();
            }

            var opponent = representation.CurrentPlayer;

            var opponentMoves = new List<ChessMove>();

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
                        opponentMoves.AddRange(GetKingMoves(representation, from, opponent));
                        break;

                    case PieceKind.Queen:
                        opponentMoves.AddRange(GetQueenMoves(representation, from, opponent));
                        break;

                    case PieceKind.Rook:
                        opponentMoves.AddRange(GetRookMoves(representation, from, opponent));
                        break;

                    case PieceKind.Bishop:
                        opponentMoves.AddRange(GetBishopMoves(representation, from, opponent));
                        break;

                    case PieceKind.Knight:
                        opponentMoves.AddRange(GetKnightMoves(representation, from, opponent));
                        break;

                    case PieceKind.Pawn:
                        opponentMoves.AddRange(GetPawnMoves(representation, from, true, opponent));
                        break;

                    default:
                        continue;
                }
            }

            return opponentMoves.Select(x => x.To).Distinct();
        }

        private bool DoesMoveCausesCheck(ChessBoard representation, ChessMove move)
        {
            return false;
        }

        private IEnumerable<ChessMove> GenerateMoves(ChessBoard representation, ChessPlayer? player = null)
        {
            if(representation == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            if (this.GetGameState(representation) != GameState.InProgress)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var possibleMoves = Positions.PositionList
                                         .Where(x => representation[x] != null && representation[x].Owner == representation.CurrentPlayer)
                                         .SelectMany(x => GetChessMoves(representation, x));

            return possibleMoves;
        }

        private IEnumerable<ChessMove> GetChessMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            switch (piece.Kind)
            {
                case PieceKind.King: return GetKingMoves(representation, from, player).Union(GetCastlings(representation, player));
                case PieceKind.Queen: return GetQueenMoves(representation, from, player);
                case PieceKind.Rook: return GetRookMoves(representation, from, player);
                case PieceKind.Bishop: return GetBishopMoves(representation, from, player);
                case PieceKind.Knight: return GetKnightMoves(representation, from, player);
                case PieceKind.Pawn: return GetPawnMoves(representation, from, false, player);
                default: return Enumerable.Empty<ChessMove>();
            }
        }

        private IEnumerable<ChessMove> GetPawnMoves(ChessBoard representation, Position from, bool onlyThreateningMoves = false, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                yield break;
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != Pieces.PieceKind.Pawn || piece.Owner != player)
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
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = stepForward
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = stepForward
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = stepForward
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackRook,
                                To = stepForward
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                To = stepForward,
                            };
                        }
                    }

                    if (doubleStepForwardOpening != null && board[doubleStepForwardOpening] == null)
                    {
                        yield return new ChessMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = false,
                            To = doubleStepForwardOpening,
                        };
                    }

                    if (captureEast != null && board[captureEast] != null && board[captureEast].Owner != player)
                    {
                        if (captureEast.Row == 1)
                        {
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = captureEast
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = captureEast
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = captureEast
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackRook,
                                To = captureEast
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                To = captureEast,
                            };
                        }
                    }

                    if (captureWest != null && board[captureWest] != null && board[captureWest].Owner != player)
                    {
                        if (captureWest.Row == 1)
                        {
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackRook,
                                To = captureWest
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                To = captureWest,
                            };
                        }
                    }

                    if (enPassantEastPosition != null
                        && captureEast != null
                        && board[captureEast] == null // This likely won't be the case
                        && board[enPassantEastPosition] != null
                        && board[enPassantEastPosition].Owner != player
                        && board[enPassantEastPosition].Kind == PieceKind.Pawn
                        && (board[enPassantEastPosition] as Pawn).IsEnPassantCapturable)
                    {
                        yield return new PawnEnPassantMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = true,
                            To = enPassantEastPosition,
                            CapturePosition = captureEast
                        };
                    }

                    if (enPassantWestPosition != null
                        && captureWest != null
                        && board[captureWest] == null // This likely won't be the case
                        && board[enPassantWestPosition] != null
                        && board[enPassantWestPosition].Owner != player
                        && board[enPassantWestPosition].Kind == PieceKind.Pawn
                        && (board[enPassantWestPosition] as Pawn).IsEnPassantCapturable)
                    {
                        yield return new PawnEnPassantMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = true,
                            To = enPassantWestPosition,
                            CapturePosition = captureWest
                        };
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
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = stepForward
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = stepForward
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = stepForward
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                PromoteTo = ChessPieces.BlackRook,
                                To = stepForward
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = false,
                                To = stepForward,
                            };
                        }
                    }

                    if (doubleStepForwardOpening != null && board[doubleStepForwardOpening] == null)
                    {
                        yield return new ChessMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = false,
                            To = doubleStepForwardOpening,
                        };
                    }

                    if (captureEast != null && board[captureEast] != null && board[captureEast].Owner != player)
                    {
                        if (captureEast.Row == 1)
                        {
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = captureEast
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = captureEast
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = captureEast
                            };


                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackRook,
                                To = captureEast
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                To = captureEast,
                            };
                        }
                    }

                    if (captureWest != null && board[captureWest] != null && board[captureWest].Owner != player)
                    {
                        if (captureWest.Row == 1)
                        {
                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackBishop,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackKnight,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackQueen,
                                To = captureWest
                            };

                            yield return new PawnPromotionalMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                PromoteTo = ChessPieces.BlackRook,
                                To = captureWest
                            };
                        }
                        else
                        {
                            yield return new ChessMove
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = true,
                                To = captureWest,
                            };
                        }
                    }

                    if (enPassantEastPosition != null
                        && captureEast != null
                        && board[captureEast] == null // This likely won't be the case
                        && board[enPassantEastPosition] != null
                        && board[enPassantEastPosition].Owner != player
                        && board[enPassantEastPosition].Kind == PieceKind.Pawn
                        && (board[enPassantEastPosition] as Pawn).IsEnPassantCapturable)
                    {
                        yield return new PawnEnPassantMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = true,
                            To = enPassantEastPosition,
                            CapturePosition = captureEast
                        };
                    }

                    if (enPassantWestPosition != null
                        && captureWest != null
                        && board[captureWest] == null // This likely won't be the case
                        && board[enPassantWestPosition] != null
                        && board[enPassantWestPosition].Owner != player
                        && board[enPassantWestPosition].Kind == PieceKind.Pawn
                        && (board[enPassantWestPosition] as Pawn).IsEnPassantCapturable)
                    {
                        yield return new PawnEnPassantMove
                        {
                            ChessPiece = piece,
                            From = from,
                            IsCaptureMove = true,
                            To = enPassantWestPosition,
                            CapturePosition = captureWest
                        };
                    }
                    break;

                default:
                    yield break;
            }
        }

        private IEnumerable<ChessMove> GetKingMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != Pieces.PieceKind.King || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var moves = from.AllDirectionsMove(1)
                .Where(x => x != null)
                .Where(x => board[x] == null || board[x].Owner != player)
                .Select(x => new ChessMove()
                {
                    ChessPiece = piece,
                    From = from,
                    IsCaptureMove = board[x] != null,
                    To = x
                }).ToList();

            return moves;
        }

        private IEnumerable<ChessMove> GetCastlings(ChessBoard representation, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                yield break;
            }

            player = player ?? representation.CurrentPlayer;
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

            var longCastlingSeemsPossible = !board[longCastlingRookPosition].HasMoved && longCastlingEmpty;


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

            var shortCastlingSeemsPossible = !board[shortCastlingRookPosition].HasMoved && shortCastlingEmpty;

            var anyCastlingPossible = shortCastlingSeemsPossible || longCastlingSeemsPossible;

            if(!anyCastlingPossible)
            {
                yield break;
            }

            var threatenedPositions = GetThreatenedPositions(representation, player.Value);

            if(!threatenedPositions.Intersect(longCastlingNoThreatPositions).Any())
            {
                yield return new KingCastlingMove()
                {
                    From = from,
                    To = player == ChessPlayer.White ? Positions.C1 : Positions.C8,
                    CastlingType = CastlingType.Long,
                    ChessPiece = piece,
                    IsCaptureMove = false
                };
            }

            if (!threatenedPositions.Intersect(longCastlingNoThreatPositions).Any())
            {
                yield return new KingCastlingMove()
                {
                    From = from,
                    To = player == ChessPlayer.White ? Positions.G1 : Positions.G8,
                    CastlingType = CastlingType.Short,
                    ChessPiece = piece,
                    IsCaptureMove = false
                };
            }
        }

        private IEnumerable<ChessMove> GetBishopMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != Pieces.PieceKind.Bishop || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(representation, from, x => x.NorthEast()));
            positions.AddRange(PositionIterate(representation, from, x => x.NorthWest()));
            positions.AddRange(PositionIterate(representation, from, x => x.SouthEast()));
            positions.AddRange(PositionIterate(representation, from, x => x.SouthWest()));

            var moves = positions.Where(x => x != null)
                                 .Where(x => board[x] == null || board[x].Owner != player)
                                 .Select(x => new ChessMove()
                                 {
                                     ChessPiece = piece,
                                     From = from,
                                     IsCaptureMove = board[x] != null,
                                     To = x
                                 });

            return moves;
        }

        private IEnumerable<Position> PositionIterate(ChessBoard representation, Position from, Func<Position, Position> positionModifier)
        {
            var board = representation;

            if (board == null)
            {
                yield break;
            }

            var piece = board[from];

            if(piece == null)
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

        private IEnumerable<ChessMove> GetQueenMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != Pieces.PieceKind.Queen || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(representation, from, x => x.North()));
            positions.AddRange(PositionIterate(representation, from, x => x.South()));
            positions.AddRange(PositionIterate(representation, from, x => x.East()));
            positions.AddRange(PositionIterate(representation, from, x => x.West()));
            positions.AddRange(PositionIterate(representation, from, x => x.NorthEast()));
            positions.AddRange(PositionIterate(representation, from, x => x.NorthWest()));
            positions.AddRange(PositionIterate(representation, from, x => x.SouthEast()));
            positions.AddRange(PositionIterate(representation, from, x => x.SouthWest()));

            return positions.Where(x => x != null)
                            .Where(x => board[x] == null || board[x].Owner != player)
                            .Select(x => new ChessMove()
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = board[x] != null,
                                To = x
                            });
        }

        private IEnumerable<ChessMove> GetRookMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != Pieces.PieceKind.Rook || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var positions = new List<Position>();
            positions.AddRange(PositionIterate(representation, from, x => x.North()));
            positions.AddRange(PositionIterate(representation, from, x => x.South()));
            positions.AddRange(PositionIterate(representation, from, x => x.East()));
            positions.AddRange(PositionIterate(representation, from, x => x.West()));

            return positions.Where(x => x != null)
                            .Where(x => board[x] == null || board[x].Owner != player)
                            .Select(x => new ChessMove()
                            {
                                ChessPiece = piece,
                                From = from,
                                IsCaptureMove = board[x] != null,
                                To = x
                            });
        }

        private IEnumerable<ChessMove> GetKnightMoves(ChessBoard representation, Position from, ChessPlayer? player = null)
        {
            var board = representation;

            if (board == null)
            {
                return Enumerable.Empty<ChessMove>();
            }

            player = player ?? representation.CurrentPlayer;

            var piece = board[from];

            if (piece == null || piece.Kind != PieceKind.Knight || piece.Owner != player)
            {
                return Enumerable.Empty<ChessMove>();
            }

            var knightMoves = from.KnightMoves();

            return knightMoves.Where(x => x != null)
                              .Where(x => board[x] == null || board[x].Owner != player)
                              .Select(x => new ChessMove()
                              {
                                  ChessPiece = piece,
                                  From = from,
                                  IsCaptureMove = board[x] != null,
                                  To = x
                              });
        }
    }
}
