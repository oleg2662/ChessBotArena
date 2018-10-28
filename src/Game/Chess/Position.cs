using System;

namespace Game.Chess
{
    [Serializable]
    /// <summary>
    /// Represents a position in the chess board.
    /// </summary>
    public sealed class Position : IEquatable<Position>
    {
        ///// <summary>
        ///// Initializes a new instance of the Position structure.
        ///// </summary>
        ///// <param name="notation">The algebraic notation of the position. (A1-H8)</param>
        //public Position(string notation) : this(GetColumnFromString(notation), GetRowFromString(notation))
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the Position structure.
        /// </summary>
        /// <param name="column">The algebraic notation of the column. (A-H)</param>
        /// <param name="row">The algebraic (1-based) notation of the row. (1-8)</param>
        public Position(char column, int row)
        {
            column = char.ToUpperInvariant(column);

            if (column < 'A' || column > 'H')
            {
                throw new ArgumentOutOfRangeException(nameof(this.Column), "The id of the column has to be between 'A' and 'H'.");
            }

            if (row < 1 || row > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Column), "The number of the column has to be between 1 and 8.");
            }

            this.Column = column;
            this.Row = row;
        }

        /// <summary>
        /// Gets the column's algebraic notation.
        /// </summary>
        public char Column { get; }

        /// <summary>
        /// Gets the row's algebraic (1-based) notation.
        /// </summary>
        public int Row { get; }

        #region Operators

        /// <summary>
        /// Compares whether two positions are considered equal.
        /// </summary>
        /// <param name="x">One of the positions.</param>
        /// <param name="y">The other position.</param>
        /// <returns>True if the two positions are considered equal (by the position they represent.) Otherwise false.</returns>
        public static bool operator ==(Position x, Position y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            var nonNullSide = x is null ? y : x;
            var possibleNullSide = y is null ? x : y;

            return nonNullSide.Equals(possibleNullSide);
        }

        /// <summary>
        /// Compares whether two positions are considered different.
        /// </summary>
        /// <param name="x">One of the positions.</param>
        /// <param name="y">The other position.</param>
        /// <returns>True if the two positions are considered different (they represent different positions on the chessboard.) Otherwise false.</returns>
        public static bool operator !=(Position obj1, Position obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Creates a position from it's algebraic notation.
        /// </summary>
        /// <param name="algebraicNotation">The position's algebraic notation.</param>
        public static explicit operator Position(string algebraicNotation)
        {
            if (algebraicNotation.Length != 2)
            {
                throw new ArgumentException("Algebraic position notation has to be 2 characters long.", nameof(algebraicNotation));
            }

            var col = algebraicNotation[0];
            int row;

            if (!int.TryParse(algebraicNotation[1].ToString(), out row))
            {
                throw new ArgumentException("Algebraic notation's second character has to be an integer.", nameof(algebraicNotation));
            }

            return new Position(col, row);
        }

        /// <summary>
        /// Creates a position from it's index in the array. Array starts from A8 and end with H1.
        /// </summary>
        /// <param name="index">The index of the position in the underlying array.</param>
        public static explicit operator Position(int index)
        {
            if (index < 0 || index > 63)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var col = (char)(index % 8 + 'A');
            var row = 8 - (index / 8);
            return new Position(col, row);
        }

        /// <summary>
        /// Gets the array index of the given position.
        /// </summary>
        /// <param name="position">The position.</param>
        public static explicit operator int(Position position)
        {
            var col = char.ToUpperInvariant(position.Column);
            var row = position.Row;

            if (col < 'A' || col > 'H' || row < 1 || row > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(col));
            }

            var idx = (8 - row) * 8 + (col - 'A');

            return idx;
        }

        /// <summary>
        /// Returns the algebraic representation of the position.
        /// </summary>
        /// <param name="position">The position.</param>
        public static explicit operator string(Position position)
        {
            return $"{position.Column}{position.Row}";
        }

        #endregion

        #region Object overrides

        /// <summary>
        /// Returns the algebraic representation of the position.
        /// </summary>
        /// <returns>Algebraic representation of the position. Example: "B2".</returns>
        public override string ToString()
        {
            return (string)this;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;
                hash = (Constants.HashXor ^ hash) ^ this.Column.GetHashCode();
                hash = (Constants.HashXor ^ hash) ^ this.Row.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as Position;

            if(obj == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        public bool Equals(Position other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Row.Equals(other.Row) && this.Column.Equals(other.Column);
        }

        #endregion

        private static char GetColumnFromString(string notation)
        {
            if (notation.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(notation), "The length of the notation has to be 2 characters long. Example: 'C6'.");
            }

            var column = notation[0];

            return column;
        }

        private static int GetRowFromString(string notation)
        {
            if (notation.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(notation), "The length of the notation has to be 2 characters long. Example: 'C6'.");
            }

            int row;
            if (!int.TryParse(notation[1].ToString(), out row))
            {
                throw new ArgumentOutOfRangeException(nameof(notation), "The second character of the notation has to be a number.");
            }

            return row;
        }
    }
}
