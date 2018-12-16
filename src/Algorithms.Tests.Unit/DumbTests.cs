using BoardGame.Algorithms.Random;
using BoardGame.Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace BoardGame.Algorithms.Tests.Unit
{
    public class RandomAlgorithmTests
    {
        [Fact]
        public void RandomAlgorithm_ReturnsMove()
        {
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new RandomAlgorithm<TestCase1.State, TestCase1.Move>(generator);

            var initState = new TestCase1.State(1, 0);
            var move1 = algorithm.Calculate(initState);

            var state2 = applier.Apply(initState, move1);
            var move2 = algorithm.Calculate(state2);

            Assert.NotNull(move1);
            Assert.NotNull(move2);
        }
    }
}
