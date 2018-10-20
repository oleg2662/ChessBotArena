using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Abstraction
{
    public interface IMechanism<TRepresentation, TPlayerMove, TGameState>
    {
        //TRepresentation Init();
        IEnumerable<TPlayerMove> GenerateMoves(TRepresentation representation);
        bool ValidateMove(TRepresentation representation, TPlayerMove move);
        TRepresentation ApplyMove(TRepresentation representation, TPlayerMove move);
        TGameState GetGameState(TRepresentation representation);
    }
}
