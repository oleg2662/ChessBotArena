using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace BoardGame.Algorithms.Tests.Unit
{
    public class MinimaxTests
    {
        [Fact]
        public void Test1()
        {
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new MinimaxAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier);
            algorithm.MaxDepth = int.MaxValue;

            var initState = new TestCase1.State(0, 0);

            var move1 = algorithm.Calculate(initState);

            Assert.Equal('b', move1.Label);
        }
    }
}
