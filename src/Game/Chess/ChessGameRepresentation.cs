//using System.Linq;
//using System.Collections.Generic;
//using Game.Abstraction;
//using Game.Chess.Moves;

//namespace Game.Chess
//{
//    public class ChessGameRepresentation : IRepresentation<ChessPlayer, ChessMove>, ICloneable<ChessGameRepresentation>
//    {
//        public ChessBoard Board { get; set; }

//        public List<ChessMove> History
//        {
//            get => Board.History;
//            set => Board.History = value;
//        }

//        public IEnumerable<ChessPlayer> Players
//        {
//            get => Board.Players;
//            set => Board.Players = value;
//        }
//        public ChessPlayer CurrentPlayer
//        {
//            get => Board.CurrentPlayer;
//            set => Board.CurrentPlayer = value;
//        }

//        public ChessGameRepresentation Clone()
//        {
//            return new ChessGameRepresentation()
//            {
//                Board = Board.Clone(),
//            };
//        }
//    }
//}
