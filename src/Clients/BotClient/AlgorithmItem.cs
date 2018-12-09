using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.BotClient
{
    public class AlgorithmItem
    {
        public AlgorithmItem(string name, Type algorithmType)
        {
            Name = name;
            AlgorithmType = algorithmType;
        }

        public string Name { get; set; }

        public Type AlgorithmType { get; set; }

        public override string ToString() => Name;
    }
}
