using System;

namespace BoardGame.Clients.BotClient
{
    public class EvaluatorItem
    {
        public EvaluatorItem(string name, Type evaluatorType)
        {
            Name = name;
            EvaluatorType = evaluatorType;
        }

        public string Name { get; set; }

        public Type EvaluatorType { get; set; }

        public override string ToString() => Name;
    }
}
