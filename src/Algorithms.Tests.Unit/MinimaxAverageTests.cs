using System.Text;
using BoardGame.Algorithms.MinimaxAverage;
using BoardGame.Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace BoardGame.Algorithms.Tests.Unit
{
    public class MinimaxAverageTests
    {
        [Fact]
        public void TestProvided_ADLreturned()
        {
            var expected = "adl";
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new MinimaxAverageAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MaxDepth = int.MaxValue,
                MinLevelAverageDepth = 2,
                MaxLevelAverageDepth = 2
            };
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
