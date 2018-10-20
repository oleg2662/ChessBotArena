namespace Game.Chess.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PositionExtensions
    {
        /// <summary>
        /// Creates a position from it's postal notation.
        /// </summary>
        /// <param name="postalNotation">The position's postal notation.</param>
        /// <returns>The position.</returns>
        public static Position ParsePostal(this string postalNotation)
        {
            if (postalNotation.Length != 2)
            {
                throw new ArgumentException("Algebraic position notation has to be 2 characters long.", nameof(postalNotation));
            }

            int col;
            int row;

            if (!int.TryParse(postalNotation[0].ToString(), out col))
            {
                throw new ArgumentException("Postal notation's first character has to be an integer.", nameof(postalNotation));
            }

            if (!int.TryParse(postalNotation[1].ToString(), out row))
            {
                throw new ArgumentException("Postal notation's second character has to be an integer.", nameof(postalNotation));
            }

            return new Position((char)('A' + col - 1), row);
        }

        public static Position North(this Position position, int number = 1)
        {
            if(number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (position is null)
            {
                return null;
            }

            var newIndex = (int)position - number * 8;

            if(newIndex < 0)
            {
                return null;
            }

            return (Position)newIndex;
        }

        public static Position South(this Position position, int number = 1)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (position is null)
            {
                return null;
            }

            var newIndex = (int)position + number * 8;

            if (newIndex >= 64)
            {
                return null;
            }

            return (Position)newIndex;
        }

        public static Position West(this Position position, int number = 1)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if(position is null)
            {
                return null;
            }

            var oldRow = position.Row;

            var newIndex = (int)position - number;

            if(newIndex < 0)
            {
                return null;
            }

            var newPosition = (Position)newIndex;

            if(newPosition.Row != oldRow)
            {
                return null;
            }

            return newPosition;
        }

        public static Position East(this Position position, int number = 1)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (position is null)
            {
                return null;
            }

            var oldRow = position.Row;

            var newIndex = (int)position + number;

            if (newIndex > 63)
            {
                return null;
            }

            var newPosition = (Position)newIndex;

            if (newPosition.Row != oldRow)
            {
                return null;
            }

            return newPosition;
        }

        public static Position NorthEast(this Position position, int number = 1)
        {
            var newPosition = position.North(number).East(number);
            return newPosition;
        }

        public static Position NorthWest(this Position position, int number = 1)
        {
            var newPosition = position.North(number).West(number);
            return newPosition;
        }

        public static Position SouthEast(this Position position, int number = 1)
        {
            var newPosition = position.South(number).East(number);
            return newPosition;
        }

        public static Position SouthWest(this Position position, int number = 1)
        {
            var newPosition = position.South(number).West(number);
            return newPosition;
        }

        public static Position KnightNorthEast(this Position position)
        {
            var newPosition = position.North(2).East(1);
            return newPosition;
        }

        public static Position KnightNorthWest(this Position position)
        {
            var newPosition = position.North(2).West(1);
            return newPosition;
        }

        public static Position KnightSouthEast(this Position position)
        {
            var newPosition = position.South(2).East(1);
            return newPosition;
        }

        public static Position KnightSouthWest(this Position position)
        {
            var newPosition = position.South(2).West(1);
            return newPosition;
        }

        public static Position KnightEastNorth(this Position position)
        {
            var newPosition = position.East(2).North(1);
            return newPosition;
        }

        public static Position KnightEastSouth(this Position position)
        {
            var newPosition = position.East(2).South(1);
            return newPosition;
        }

        public static Position KnightWestNorth(this Position position)
        {
            var newPosition = position.West(2).North(1);
            return newPosition;
        }

        public static Position KnightWestSouth(this Position position)
        {
            var newPosition = position.West(2).South(1);
            return newPosition;
        }

        public static IEnumerable<Position> KnightMoves(this Position position)
        {
            var possibleMoves = new []
            {
                position.KnightSouthWest(),
                position.KnightSouthEast(),
                position.KnightWestSouth(),
                position.KnightWestNorth(),
                position.KnightEastSouth(),
                position.KnightEastNorth(),
                position.KnightNorthWest(),
                position.KnightNorthEast()
            }.Where(x => x != null);

            return possibleMoves;
        }

        public static IEnumerable<Position> Westwards(this Position position, int maxRadius = int.MaxValue)
        {
            var west = position.West();
            while (west != null && maxRadius-- > 0)
            {
                yield return west;
                west = west.West();
            }
        }

        public static IEnumerable<Position> Eastwards(this Position position, int maxRadius = int.MaxValue)
        {
            var east = position.East();
            while (east != null && maxRadius-- > 0)
            {
                yield return east;
                east = east.East();
            }
        }

        public static IEnumerable<Position> Northwards(this Position position, int maxRadius = int.MaxValue)
        {
            var north = position.North();
            while (north != null && maxRadius-- > 0)
            {
                yield return north;
                north = north.North();
            }
        }

        public static IEnumerable<Position> Southtwards(this Position position, int maxRadius = int.MaxValue)
        {
            var south = position.South();
            while (south != null && maxRadius-- > 0)
            {
                yield return south;
                south = south.South();
            }
        }

        public static IEnumerable<Position> NorthEastwards(this Position position, int maxRadius = int.MaxValue)
        {
            var northEast = position.NorthEast();
            while(northEast != null && maxRadius-- > 0)
            {
                yield return northEast;
                northEast = northEast.NorthEast();
            }
        }

        public static IEnumerable<Position> NorthWestwards(this Position position, int maxRadius = int.MaxValue)
        {
            var northWest = position.NorthWest();
            while (northWest != null && maxRadius-- > 0)
            {
                yield return northWest;
                northWest = northWest.NorthWest();
            }
        }

        public static IEnumerable<Position> SouthEastwards(this Position position, int maxRadius = int.MaxValue)
        {
            var southEast = position.SouthEast();
            while (southEast != null && maxRadius-- > 0)
            {
                yield return southEast;
                southEast = southEast.SouthEast();
            }
        }

        public static IEnumerable<Position> SouthWestwards(this Position position, int maxRadius = int.MaxValue)
        {
            var southWest = position.SouthWest();
            while (southWest != null && maxRadius-- > 0)
            {
                yield return southWest;
                southWest = southWest.SouthWest();
            }
        }

        public static IEnumerable<Position> StraightMoves(this Position position, int maxRadius = int.MaxValue)
        {
            return position.Northwards(maxRadius)
                .Union(position.Southtwards(maxRadius))
                .Union(position.Eastwards(maxRadius))
                .Union(position.Westwards(maxRadius));
        }

        public static IEnumerable<Position> DiagonalMoves(this Position position, int maxRadius = int.MaxValue)
        {
            return position.NorthEastwards(maxRadius)
                .Union(position.SouthEastwards(maxRadius))
                .Union(position.NorthWestwards(maxRadius))
                .Union(position.SouthWestwards(maxRadius));
        }

        public static IEnumerable<Position> AllDirectionsMove(this Position position, int maxRadius = int.MaxValue)
        {
            return position.StraightMoves(maxRadius)
                .Union(position.DiagonalMoves(maxRadius));
        }
    }
}
