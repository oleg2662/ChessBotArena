using System.Collections.Generic;

namespace Game.Abstraction
{
    public interface IMechanism<TRepresentation, TPlayerMove, out TGameState>
    {
        IEnumerable<TPlayerMove> GenerateMoves(TRepresentation representation);
        bool ValidateMove(TRepresentation representation, TPlayerMove move);
        TRepresentation ApplyMove(TRepresentation representation, TPlayerMove move);
        TGameState GetGameState(TRepresentation representation);
    }
}
