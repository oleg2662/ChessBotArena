using System.Collections.Generic;

namespace BoardGame.Game.Chess
{
    public static class Positions
    {
        public static Position A1 { get; } = new Position('A', 1);
        public static Position A2 { get; } = new Position('A', 2);
        public static Position A3 { get; } = new Position('A', 3);
        public static Position A4 { get; } = new Position('A', 4);
        public static Position A5 { get; } = new Position('A', 5);
        public static Position A6 { get; } = new Position('A', 6);
        public static Position A7 { get; } = new Position('A', 7);
        public static Position A8 { get; } = new Position('A', 8);

        public static Position B1 { get; } = new Position('B', 1);
        public static Position B2 { get; } = new Position('B', 2);
        public static Position B3 { get; } = new Position('B', 3);
        public static Position B4 { get; } = new Position('B', 4);
        public static Position B5 { get; } = new Position('B', 5);
        public static Position B6 { get; } = new Position('B', 6);
        public static Position B7 { get; } = new Position('B', 7);
        public static Position B8 { get; } = new Position('B', 8);

        public static Position C1 { get; } = new Position('C', 1);
        public static Position C2 { get; } = new Position('C', 2);
        public static Position C3 { get; } = new Position('C', 3);
        public static Position C4 { get; } = new Position('C', 4);
        public static Position C5 { get; } = new Position('C', 5);
        public static Position C6 { get; } = new Position('C', 6);
        public static Position C7 { get; } = new Position('C', 7);
        public static Position C8 { get; } = new Position('C', 8);

        public static Position D1 { get; } = new Position('D', 1);
        public static Position D2 { get; } = new Position('D', 2);
        public static Position D3 { get; } = new Position('D', 3);
        public static Position D4 { get; } = new Position('D', 4);
        public static Position D5 { get; } = new Position('D', 5);
        public static Position D6 { get; } = new Position('D', 6);
        public static Position D7 { get; } = new Position('D', 7);
        public static Position D8 { get; } = new Position('D', 8);

        public static Position E1 { get; } = new Position('E', 1);
        public static Position E2 { get; } = new Position('E', 2);
        public static Position E3 { get; } = new Position('E', 3);
        public static Position E4 { get; } = new Position('E', 4);
        public static Position E5 { get; } = new Position('E', 5);
        public static Position E6 { get; } = new Position('E', 6);
        public static Position E7 { get; } = new Position('E', 7);
        public static Position E8 { get; } = new Position('E', 8);

        public static Position F1 { get; } = new Position('F', 1);
        public static Position F2 { get; } = new Position('F', 2);
        public static Position F3 { get; } = new Position('F', 3);
        public static Position F4 { get; } = new Position('F', 4);
        public static Position F5 { get; } = new Position('F', 5);
        public static Position F6 { get; } = new Position('F', 6);
        public static Position F7 { get; } = new Position('F', 7);
        public static Position F8 { get; } = new Position('F', 8);

        public static Position G1 { get; } = new Position('G', 1);
        public static Position G2 { get; } = new Position('G', 2);
        public static Position G3 { get; } = new Position('G', 3);
        public static Position G4 { get; } = new Position('G', 4);
        public static Position G5 { get; } = new Position('G', 5);
        public static Position G6 { get; } = new Position('G', 6);
        public static Position G7 { get; } = new Position('G', 7);
        public static Position G8 { get; } = new Position('G', 8);

        public static Position H1 { get; } = new Position('H', 1);
        public static Position H2 { get; } = new Position('H', 2);
        public static Position H3 { get; } = new Position('H', 3);
        public static Position H4 { get; } = new Position('H', 4);
        public static Position H5 { get; } = new Position('H', 5);
        public static Position H6 { get; } = new Position('H', 6);
        public static Position H7 { get; } = new Position('H', 7);
        public static Position H8 { get; } = new Position('H', 8);

        public static IEnumerable<Position> PositionList
        {
            get
            {
                yield return A1;
                yield return A2;
                yield return A3;
                yield return A4;
                yield return A5;
                yield return A6;
                yield return A7;
                yield return A8;
                yield return B1;
                yield return B2;
                yield return B3;
                yield return B4;
                yield return B5;
                yield return B6;
                yield return B7;
                yield return B8;
                yield return C1;
                yield return C2;
                yield return C3;
                yield return C4;
                yield return C5;
                yield return C6;
                yield return C7;
                yield return C8;
                yield return D1;
                yield return D2;
                yield return D3;
                yield return D4;
                yield return D5;
                yield return D6;
                yield return D7;
                yield return D8;
                yield return E1;
                yield return E2;
                yield return E3;
                yield return E4;
                yield return E5;
                yield return E6;
                yield return E7;
                yield return E8;
                yield return F1;
                yield return F2;
                yield return F3;
                yield return F4;
                yield return F5;
                yield return F6;
                yield return F7;
                yield return F8;
                yield return G1;
                yield return G2;
                yield return G3;
                yield return G4;
                yield return G5;
                yield return G6;
                yield return G7;
                yield return G8;
                yield return H1;
                yield return H2;
                yield return H3;
                yield return H4;
                yield return H5;
                yield return H6;
                yield return H7;
                yield return H8;
            }
        }
    }
}
