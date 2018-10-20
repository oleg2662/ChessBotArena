using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Chess
{
    public interface IChessBoardInitializer
    {
        ChessBoard Create();
    }
}
