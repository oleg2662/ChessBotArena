using System.Collections.Generic;
using BoardGame.Game.Chess;
using Xunit;

namespace BoardGame.Game.Tests.Unit.Chess
{
    public class PositionTests
    {
        [Theory]
        [MemberData(nameof(PositionEqualityFunctionTestdata))]
        public void PositionEquality_EqualityOperatorCheck(Position p1, Position p2, bool expectedResult)
        {
            // Act
            var equality = (p1 == p2);

            // Assert
            Assert.Equal(expectedResult, equality);
        }

        [Theory]
        [MemberData(nameof(PositionEqualityFunctionTestdata))]
        public void PositionEquality_EqualityFunctionCheck(Position p1, Position p2, bool expectedResult)
        {
            // Act
            var equality = (p1.Equals(p2));

            // Assert
            Assert.Equal(expectedResult, equality);
        }

        public static IEnumerable<object[]> PositionEqualityOperatorTestdata =>
            new List<object[]>
            {
                    new object[] { Positions.A1, new Position('A', 1), true },
                    new object[] { Positions.B5, new Position('B', 5), true },
                    new object[] { Positions.H8, new Position('H', 8), true },
                    new object[] { Positions.F4, new Position('E', 5), false },
                    new object[] { (Position)"A1", new Position('A', 1), true },
                    new object[] { (Position)"B1", new Position('B', 5), false },
                    new object[] { (Position)"H8", new Position('H', 8), true },
                    new object[] { (Position)"F4", new Position('E', 5), false },
                    new object[] { null, new Position('E', 5), false },
                    new object[] { Positions.F6, null, false },
                    new object[] { null, null, false },
            };

        public static IEnumerable<object[]> PositionEqualityFunctionTestdata =>
            new List<object[]>
            {
                    new object[] { Positions.A1, new Position('A', 1), true },
                    new object[] { Positions.B5, new Position('B', 5), true },
                    new object[] { Positions.H8, new Position('H', 8), true },
                    new object[] { Positions.F4, new Position('E', 5), false },
                    new object[] { (Position)"A1", new Position('A', 1), true },
                    new object[] { (Position)"B1", new Position('B', 5), false },
                    new object[] { (Position)"H8", new Position('H', 8), true },
                    new object[] { (Position)"F4", new Position('E', 5), false },
                    new object[] { Positions.F6, null, false },
            };
    }
}
