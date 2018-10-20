using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Abstraction
{
    public interface IRepresentation<TPlayer, TMove>
    {
        List<TMove> History { get; set; }
        IEnumerable<TPlayer> Players { get; set; }
        TPlayer CurrentPlayer { get; set; }
    }
}
