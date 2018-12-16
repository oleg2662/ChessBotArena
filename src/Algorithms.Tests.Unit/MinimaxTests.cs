using System.Text;
using BoardGame.Algorithms.AlphaBeta;
using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.MinimaxAverage;
using BoardGame.Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace BoardGame.Algorithms.Tests.Unit
{
    public class MinimaxTests
    {
        [Fact]
        public void TestProvided_BENreturned()
        {
            var expected = "ben";
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new MinimaxAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier);
            algorithm.MaxDepth = int.MaxValue;
            var sb = new StringBuilder();

            var state = new TestCase1.State(1, 0);
            var move = algorithm.Calculate(state);
            sb.Append(move.Label);

            state = applier.Apply(state, move);
            move = algorithm.Calculate(state);
            sb.Append(move.Label);

            state = applier.Apply(state, move);
            move = algorithm.Calculate(state);
            sb.Append(move.Label);

            var actual = sb.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
