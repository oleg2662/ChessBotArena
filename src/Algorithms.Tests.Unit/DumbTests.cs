using Algorithms.Dumb;
using Algorithms.Tests.Unit.TestCaseClasses;
using Xunit;

namespace Algorithms.Tests.Unit
{
    public class DumbTests
    {
        [Fact]
        public void Test1()
        {
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new DumbAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier);

            var initState = new TestCase1.State(0, 0);
            var move1 = algorithm.Calculate(initState, true);

            var state2 = applier.Apply(initState, move1);
            var move2 = algorithm.Calculate(state2, false);

            Assert.NotNull(move1);
            Assert.NotNull(move2);
        }
    }
}
