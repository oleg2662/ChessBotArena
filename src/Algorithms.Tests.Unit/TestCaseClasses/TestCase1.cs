using System;
using System.Collections.Generic;
using BoardGame.Algorithms.Abstractions.Interfaces;

namespace BoardGame.Algorithms.Tests.Unit.TestCaseClasses
{
    internal static class TestCase1
    {
        internal class State
        {
            public State(int id, int value)
            {
                Id = id;
                Value = value;
            }

            public int Id { get; set; }
            public int Value { get; set; }
        }

        internal class Move : IEquatable<Move>
        {
            public Move(char label)
            {
                Label = label;
            }

            public char Label { get; }

            public bool Equals(Move other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Label == other.Label;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Move) obj);
            }

            public override int GetHashCode()
            {
                return Label.GetHashCode();
            }

            public static bool operator ==(Move left, Move right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Move left, Move right)
            {
                return !Equals(left, right);
            }
        }

        internal class Evaluator : IEvaluator<State>
        {
            public int Evaluate(State state)
            {
                return state.Value;
            }
        }

        internal class Generator : IGenerator<State, Move>
        {
            public IEnumerable<Move> Generate(State state)
            {
                switch (state.Id)
                {
                    case 1:
                        yield return new Move('a');
                        yield return new Move('b');
                        break;

                    case 2:
                        yield return new Move('c');
                        yield return new Move('d');
                        break;

                    case 3:
                        yield return new Move('e');
                        break;

                    case 4:
                        yield return new Move('f');
                        yield return new Move('g');
                        yield return new Move('h');
                        yield return new Move('i');
                        break;

                    case 5:
                        yield return new Move('j');
                        yield return new Move('k');
                        yield return new Move('l');
                        break;

                    case 6:
                        yield return new Move('m');
                        yield return new Move('n');
                        yield return new Move('o');
                        yield return new Move('p');
                        break;

                    default:
                        break;
                }
            }
        }

        internal class Applier : IApplier<State, Move>
        {
            public State Apply(State state, Move move)
            {
                switch (move.Label)
                {
                    case 'a': return new State(2, 2);
                    case 'b': return new State(3, 3);
                    case 'c': return new State(4, 4);
                    case 'd': return new State(5, 5);
                    case 'e': return new State(6, int.MinValue);
                    case 'f': return new State(7, 8);
                    case 'g': return new State(8, 0);
                    case 'h': return new State(9, 5);
                    case 'i': return new State(10, 6);
                    case 'j': return new State(11, 7);
                    case 'k': return new State(12, 4);
                    case 'l': return new State(13, 8);
                    case 'm': return new State(14, -1);
                    case 'n': return new State(15, 9);
                    case 'o': return new State(16, -1);
                    case 'p': return new State(17, -2);
                }

                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
