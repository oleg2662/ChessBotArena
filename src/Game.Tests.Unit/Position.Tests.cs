using System.Collections;
using System.Collections.Generic;

using Game.Chess;
using Xunit;

namespace Game.Tests.Unit
{
    public class PositionTests
    {
        [Theory]
        [ClassData(typeof(PositionAndArrayIndexMapping))]
        public void PositionConversion_FromArrayIndexToPosition_ExpectedPositionReturned(int arrayIndex, Position position)
        {
            // Arrange and Act
            var result = (Position)arrayIndex;

            // Assert
            Assert.Equal(position, result);
        }

        [Theory]
        [ClassData(typeof(PositionAndArrayIndexMapping))]
        public void PositionConversion_FromPositionToArrayIndex_ExpectedArrayIndexReturned(int arrayIndex, Position position)
        {
            // Arrange and Act
            var result = (int)position;

            // Assert
            Assert.Equal(arrayIndex, result);
        }

        [Theory]
        [ClassData(typeof(PositionAndStringRepresentation))]
        public void PositionConversion_FromPositionToString_ExpectedStringReturned(Position position, string expected)
        {
            // Arrange and Act
            var result = (string)position;

            // Assert
            Assert.Equal(expected, result);
        }

        private class PositionAndArrayIndexMapping : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {  0, Positions.A8 };
                yield return new object[] {  1, Positions.B8 };
                yield return new object[] {  2, Positions.C8 };
                yield return new object[] {  3, Positions.D8 };
                yield return new object[] {  4, Positions.E8 };
                yield return new object[] {  5, Positions.F8 };
                yield return new object[] {  6, Positions.G8 };
                yield return new object[] {  7, Positions.H8 };

                yield return new object[] {  8, Positions.A7 };
                yield return new object[] {  9, Positions.B7 };
                yield return new object[] { 10, Positions.C7 };
                yield return new object[] { 11, Positions.D7 };
                yield return new object[] { 12, Positions.E7 };
                yield return new object[] { 13, Positions.F7 };
                yield return new object[] { 14, Positions.G7 };
                yield return new object[] { 15, Positions.H7 };

                yield return new object[] { 16, Positions.A6 };
                yield return new object[] { 17, Positions.B6 };
                yield return new object[] { 18, Positions.C6 };
                yield return new object[] { 19, Positions.D6 };
                yield return new object[] { 20, Positions.E6 };
                yield return new object[] { 21, Positions.F6 };
                yield return new object[] { 22, Positions.G6 };
                yield return new object[] { 23, Positions.H6 };

                yield return new object[] { 24, Positions.A5 };
                yield return new object[] { 25, Positions.B5 };
                yield return new object[] { 26, Positions.C5 };
                yield return new object[] { 27, Positions.D5 };
                yield return new object[] { 28, Positions.E5 };
                yield return new object[] { 29, Positions.F5 };
                yield return new object[] { 30, Positions.G5 };
                yield return new object[] { 31, Positions.H5 };

                yield return new object[] { 32, Positions.A4 };
                yield return new object[] { 33, Positions.B4 };
                yield return new object[] { 34, Positions.C4 };
                yield return new object[] { 35, Positions.D4 };
                yield return new object[] { 36, Positions.E4 };
                yield return new object[] { 37, Positions.F4 };
                yield return new object[] { 38, Positions.G4 };
                yield return new object[] { 39, Positions.H4 };

                yield return new object[] { 40, Positions.A3 };
                yield return new object[] { 41, Positions.B3 };
                yield return new object[] { 42, Positions.C3 };
                yield return new object[] { 43, Positions.D3 };
                yield return new object[] { 44, Positions.E3 };
                yield return new object[] { 45, Positions.F3 };
                yield return new object[] { 46, Positions.G3 };
                yield return new object[] { 47, Positions.H3 };

                yield return new object[] { 48, Positions.A2 };
                yield return new object[] { 49, Positions.B2 };
                yield return new object[] { 50, Positions.C2 };
                yield return new object[] { 51, Positions.D2 };
                yield return new object[] { 52, Positions.E2 };
                yield return new object[] { 53, Positions.F2 };
                yield return new object[] { 54, Positions.G2 };
                yield return new object[] { 55, Positions.H2 };

                yield return new object[] { 56, Positions.A1 };
                yield return new object[] { 57, Positions.B1 };
                yield return new object[] { 58, Positions.C1 };
                yield return new object[] { 59, Positions.D1 };
                yield return new object[] { 60, Positions.E1 };
                yield return new object[] { 61, Positions.F1 };
                yield return new object[] { 62, Positions.G1 };
                yield return new object[] { 63, Positions.H1 };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }

        private class PositionAndStringRepresentation : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Positions.A8, "A8" };
                yield return new object[] { Positions.B8, "B8" };
                yield return new object[] { Positions.C8, "C8" };
                yield return new object[] { Positions.D8, "D8" };
                yield return new object[] { Positions.E8, "E8" };
                yield return new object[] { Positions.F8, "F8" };
                yield return new object[] { Positions.G8, "G8" };
                yield return new object[] { Positions.H8, "H8" };

                yield return new object[] { Positions.A7, "A7" };
                yield return new object[] { Positions.B7, "B7" };
                yield return new object[] { Positions.C7, "C7" };
                yield return new object[] { Positions.D7, "D7" };
                yield return new object[] { Positions.E7, "E7" };
                yield return new object[] { Positions.F7, "F7" };
                yield return new object[] { Positions.G7, "G7" };
                yield return new object[] { Positions.H7, "H7" };

                yield return new object[] { Positions.A6, "A6" };
                yield return new object[] { Positions.B6, "B6" };
                yield return new object[] { Positions.C6, "C6" };
                yield return new object[] { Positions.D6, "D6" };
                yield return new object[] { Positions.E6, "E6" };
                yield return new object[] { Positions.F6, "F6" };
                yield return new object[] { Positions.G6, "G6" };
                yield return new object[] { Positions.H6, "H6" };

                yield return new object[] { Positions.A5, "A5" };
                yield return new object[] { Positions.B5, "B5" };
                yield return new object[] { Positions.C5, "C5" };
                yield return new object[] { Positions.D5, "D5" };
                yield return new object[] { Positions.E5, "E5" };
                yield return new object[] { Positions.F5, "F5" };
                yield return new object[] { Positions.G5, "G5" };
                yield return new object[] { Positions.H5, "H5" };

                yield return new object[] { Positions.A4, "A4" };
                yield return new object[] { Positions.B4, "B4" };
                yield return new object[] { Positions.C4, "C4" };
                yield return new object[] { Positions.D4, "D4" };
                yield return new object[] { Positions.E4, "E4" };
                yield return new object[] { Positions.F4, "F4" };
                yield return new object[] { Positions.G4, "G4" };
                yield return new object[] { Positions.H4, "H4" };

                yield return new object[] { Positions.A3, "A3" };
                yield return new object[] { Positions.B3, "B3" };
                yield return new object[] { Positions.C3, "C3" };
                yield return new object[] { Positions.D3, "D3" };
                yield return new object[] { Positions.E3, "E3" };
                yield return new object[] { Positions.F3, "F3" };
                yield return new object[] { Positions.G3, "G3" };
                yield return new object[] { Positions.H3, "H3" };

                yield return new object[] { Positions.A2, "A2" };
                yield return new object[] { Positions.B2, "B2" };
                yield return new object[] { Positions.C2, "C2" };
                yield return new object[] { Positions.D2, "D2" };
                yield return new object[] { Positions.E2, "E2" };
                yield return new object[] { Positions.F2, "F2" };
                yield return new object[] { Positions.G2, "G2" };
                yield return new object[] { Positions.H2, "H2" };

                yield return new object[] { Positions.A1, "A1" };
                yield return new object[] { Positions.B1, "B1" };
                yield return new object[] { Positions.C1, "C1" };
                yield return new object[] { Positions.D1, "D1" };
                yield return new object[] { Positions.E1, "E1" };
                yield return new object[] { Positions.F1, "F1" };
                yield return new object[] { Positions.G1, "G1" };
                yield return new object[] { Positions.H1, "H1" };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
