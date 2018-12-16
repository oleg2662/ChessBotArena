using System.Text;
using BoardGame.Algorithms.AlphaBeta;
using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.MinimaxAverage;
using BoardGame.Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace BoardGame.Algorithms.Tests.Unit
{
    public class EqualityTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        public void MaxLevel1(int depth)
        {
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();

            var algorithm1 = new MinimaxAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MaxDepth = depth
            };
            var algorithm2 = new MinimaxAverageAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MaxDepth = depth,
                MaxLevelAverageDepth = 1,
                MinLevelAverageDepth = 1
            };
            var algorithm3 = new AlphaBetaAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MaxDepth = 1
            };

            var state = new TestCase1.State(0, 0);

            var move1 = algorithm1.Calculate(state);
            var move2 = algorithm2.Calculate(state);
            var move3 = algorithm3.Calculate(state);

            Assert.Equal(move1, move2);
            Assert.Equal(move2, move3);
            Assert.Equal(move3, move1);
        }
    }
}
