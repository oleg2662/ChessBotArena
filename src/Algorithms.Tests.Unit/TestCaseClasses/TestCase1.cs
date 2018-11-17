using System;
using System.Collections.Generic;
using BoardGame.Model.Abstractions.Interfaces;

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

        internal class Move
        {
            public Move(char label)
            {
                Label = label;
            }

            public char Label { get; set; }
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
                    case 0:
                        yield return new Move('a');
                        yield return new Move('b');
                        break;

                    case 1:
                        yield return new Move('c');
                        yield return new Move('d');
                        break;

                    case 2:
                        yield return new Move('e');
                        yield return new Move('f');
                        break;

                    case 3:
                        yield return new Move('g');
                        yield return new Move('h');
                        break;

                    case 4:
                        yield return new Move('i');
                        break;

                    case 5:
                        yield return new Move('j');
                        yield return new Move('k');
                        break;

                    case 6:
                        yield return new Move('l');
                        break;

                    case 7:
                        yield return new Move('m');
                        yield return new Move('n');
                        break;

                    case 8:
                        yield return new Move('o');
                        break;

                    case 9:
                        yield return new Move('p');
                        break;

                    case 10:
                        yield return new Move('q');
                        yield return new Move('r');
                        break;

                    case 11:
                        yield return new Move('s');
                        break;

                    case 12:
                        yield return new Move('t');
                        yield return new Move('u');
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
                    case 'a': return new State(1, 2);
                    case 'b': return new State(2, 4);
                    case 'c': return new State(3, 8);
                    case 'd': return new State(4, 7);
                    case 'e': return new State(5, 2);
                    case 'f': return new State(6, 5);
                    case 'g': return new State(7, 9);
                    case 'h': return new State(8, 2);
                    case 'i': return new State(9, 1);
                    case 'j': return new State(10, 3);
                    case 'k': return new State(11, 4);
                    case 'l': return new State(12, 5);
                    case 'm': return new State(13, 10);
                    case 'n': return new State(14, int.MaxValue);
                    case 'o': return new State(15, 5);
                    case 'p': return new State(16, -10);
                    case 'q': return new State(17, 7);
                    case 'r': return new State(18, 5);
                    case 's': return new State(19, int.MinValue);
                    case 't': return new State(20, -7);
                    case 'u': return new State(21, -5);
                }

                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
