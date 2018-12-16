using System.Text;
using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.MinimaxAverage;
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

        [Fact]
        public void Test2()
        {
            var expected = "bflt";
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm = new MinimaxAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier);
            algorithm.MaxDepth = int.MaxValue;
            var sb = new StringBuilder();

            var state = new TestCase1.State(0, 0);
            var move = algorithm.Calculate(state);
            sb.Append(move.Label);

            state = applier.Apply(state, move);
            move = algorithm.Calculate(state);
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

        [Fact]
        public void Test3()
        {
            var evaluator = new TestCase1.Evaluator();
            var generator = new TestCase1.Generator();
            var applier = new TestCase1.Applier();
            var algorithm1 = new MinimaxAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MaxDepth = int.MaxValue
            };
            var algorithm2 = new MinimaxAverageAlgorithm<TestCase1.State, TestCase1.Move>(evaluator, generator, applier)
            {
                MinLevelAverageDepth = 1,
                MaxLevelAverageDepth = 1,
                MaxDepth = int.MaxValue
            };

            var initState1 = new TestCase1.State(0, 0);
            var initState2 = new TestCase1.State(0, 0);

            var move11 = algorithm1.Calculate(initState1);
            var move21 = algorithm2.Calculate(initState2);

            Assert.Equal('b', move11.Label);
            Assert.Equal('b', move21.Label);
        }
    }
}
