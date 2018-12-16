using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;

namespace BoardGame.ChessboardVisualizer.TestApp
{
    class TestConsole
    {
        static void Main(string[] args)
        {
            var board = new ChessRepresentationInitializer().Create();

            board.CurrentPlayer = ChessPlayer.White;

            var manager = new ChessMechanism();

            var step1 = new ChessMove(ChessPlayer.White, (Position) "B2", (Position) "B4");
            var step2 = new ChessMove(ChessPlayer.Black, (Position) "E7", (Position) "E5");
            var step3 = new ChessMove(ChessPlayer.White, (Position) "B4", (Position) "B5");
            var step4 = new ChessMove(ChessPlayer.Black, (Position) "B8", (Position) "C6");
            var step5 = new ChessMove(ChessPlayer.White, (Position) "B5", (Position) "C6");

            board = manager.ApplyMove(board, step1);
            board = manager.ApplyMove(board, step2);
            board = manager.ApplyMove(board, step3);
            board = manager.ApplyMove(board, step4);
            board = manager.ApplyMove(board, step5);

            Tools.ChessboardVisualizer.ChessboardVisualizer.TestShowVisualizer(board);
        }
    }
}
