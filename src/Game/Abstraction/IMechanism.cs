using System.Collections.Generic;
using Game.Chess;

namespace Game.Abstraction
{
    public interface IMechanism<TRepresentation, TPlayerMove, out TGameState>
    {
        IEnumerable<TPlayerMove> GenerateMoves(TRepresentation representation, ChessPlayer? player);
        bool ValidateMove(TRepresentation representation, TPlayerMove move);
        TRepresentation ApplyMove(TRepresentation representation, TPlayerMove move);
        TGameState GetGameState(TRepresentation representation);
    }
}
