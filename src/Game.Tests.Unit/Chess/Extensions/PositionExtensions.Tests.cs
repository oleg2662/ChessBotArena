using System.Collections.Generic;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Extensions;
using Xunit;

namespace BoardGame.Game.Tests.Unit.Chess.Extensions
{
    public class PositionExtensions
    {
        [Theory]
        [MemberData(nameof(NorthTests))]
        public void NorthFunctionTest(Position p, int steps, Position expected)
        {
            var result = p.North(steps);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(NorthTestsNull))]
        public void NorthFunctionNullTest(Position p, int steps)
        {
            var result = p.North(steps);
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(SouthTests))]
        public void SouthFunctionTest(Position p, int steps, Position expected)
        {
            var result = p.South(steps);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(SouthTestsNull))]
        public void SouthFunctionNullTest(Position p, int steps)
        {
            var result = p.South(steps);
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(EastTests))]
        public void EastFunctionTest(Position p, int steps, Position expected)
        {
            var result = p.East(steps);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(EastTestsNull))]
        public void EastFunctionNullTest(Position p, int steps)
        {
            var result = p.East(steps);
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(WestTests))]
        public void WestFunctionTest(Position p, int steps, Position expected)
        {
            var result = p.West(steps);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(WestTestsNull))]
        public void WestFunctionNullTest(Position p, int steps)
        {
            var result = p.West(steps);
            Assert.Null(result);
        }

        [Fact]
        public void ChainTest()
        {
            var expected = Positions.E4;
            var result = expected.NorthEast().East().SouthEast().South().SouthWest().West().NorthWest().North();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ChainTestNull()
        {
            var result = Positions.E4
                            .NorthEast()
                            .East()
                            .SouthEast()
                            .South()
                            .SouthWest()
                            .West()
                            .NorthWest()
                            .North()
                            .North()
                            .North(2)
                            .North(3)
                            .North(4);

            Assert.Null(result);
        }

        public static IEnumerable<object[]> NorthTests =>
            new List<object[]>
            {
                new object[] { Positions.A1, 1, Positions.A2 },
                new object[] { Positions.B1, 2, Positions.B3 },
                new object[] { Positions.C1, 3, Positions.C4 },
                new object[] { Positions.D1, 4, Positions.D5 },
                new object[] { Positions.E1, 5, Positions.E6 },
                new object[] { Positions.F1, 6, Positions.F7 },
                new object[] { Positions.G1, 7, Positions.G8 },
                new object[] { Positions.H1, 7, Positions.H8 },
            };

        public static IEnumerable<object[]> NorthTestsNull =>
            new List<object[]>
            {
                new object[] { Positions.A1, 8 },
                new object[] { Positions.A1, 10 },
                new object[] { Positions.B8, 1 },
                new object[] { null, 1 },
            };

        public static IEnumerable<object[]> SouthTests =>
            new List<object[]>
            {
                new object[] { Positions.A8, 1, Positions.A7 },
                new object[] { Positions.B8, 2, Positions.B6 },
                new object[] { Positions.C8, 3, Positions.C5 },
                new object[] { Positions.D8, 4, Positions.D4 },
                new object[] { Positions.E8, 5, Positions.E3 },
                new object[] { Positions.F8, 6, Positions.F2 },
                new object[] { Positions.G8, 7, Positions.G1 },
                new object[] { Positions.H8, 7, Positions.H1 },
            };

        public static IEnumerable<object[]> SouthTestsNull =>
            new List<object[]>
            {
                new object[] { Positions.A8, 8 },
                new object[] { Positions.A8, 10 },
                new object[] { Positions.B1, 1 },
                new object[] { null, 1 },
            };

        public static IEnumerable<object[]> EastTests =>
            new List<object[]>
            {
                new object[] { Positions.A1, 7, Positions.H1 },
                new object[] { Positions.B2, 6, Positions.H2 },
                new object[] { Positions.C3, 5, Positions.H3 },
                new object[] { Positions.D4, 4, Positions.H4 },
                new object[] { Positions.E5, 3, Positions.H5 },
                new object[] { Positions.F6, 2, Positions.H6 },
                new object[] { Positions.G7, 1, Positions.H7 },
            };

        public static IEnumerable<object[]> EastTestsNull =>
            new List<object[]>
            {
                new object[] { Positions.A8, 8 },
                new object[] { Positions.A8, 10 },
                new object[] { Positions.H3, 1 },
                new object[] { null, 1 },
            };

        public static IEnumerable<object[]> WestTests =>
            new List<object[]>
            {
                new object[] { Positions.B2, 1, Positions.A2 },
                new object[] { Positions.C3, 2, Positions.A3 },
                new object[] { Positions.D4, 3, Positions.A4 },
                new object[] { Positions.E5, 4, Positions.A5 },
                new object[] { Positions.F6, 5, Positions.A6 },
                new object[] { Positions.G7, 6, Positions.A7 },
                new object[] { Positions.H8, 7, Positions.A8 },
            };

        public static IEnumerable<object[]> WestTestsNull =>
            new List<object[]>
            {
                new object[] { Positions.H8, 8 },
                new object[] { Positions.G6, 10 },
                new object[] { Positions.A4, 1 },
                new object[] { null, 1 },
            };
    }
}
