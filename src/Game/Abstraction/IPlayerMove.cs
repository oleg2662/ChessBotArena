using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Abstraction
{
    public interface IPlayerMove<TPlayer, TMove>
    {
        TPlayer Owner { get; set; }
        TMove Move { get; set; }
    }
}
