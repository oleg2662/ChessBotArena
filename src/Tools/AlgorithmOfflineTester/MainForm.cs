using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.AlphaBeta;
using BoardGame.Algorithms.Greedy;
using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.MinimaxAverage;
using BoardGame.Algorithms.Random;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;

namespace BoardGame.Tools.AlgorithmOfflineTester
{
    public partial class MainForm : Form
    {
        private readonly IEvaluator<ChessRepresentation> _evaluator;
        private readonly IGenerator<ChessRepresentation, BaseMove> _generator;
        private readonly IApplier<ChessRepresentation, BaseMove> _applier;

        private IAlgorithm<ChessRepresentation, BaseMove> _algorithmRight;
        private IAlgorithm<ChessRepresentation, BaseMove> _algorithmLeft;

        private static readonly Random Randomizer = new Random();
        private bool _isActive = false;

        private IAlgorithm<ChessRepresentation, BaseMove> CreateAlgorithm(Algorithms selectedAlgorithm, int maxDepth)
        {
            switch (selectedAlgorithm)
            {
                case Algorithms.Minimax:
                    return new MinimaxAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };

                case Algorithms.MinimaxAverage:
                    return new MinimaxAverageAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };

                case Algorithms.AlphaBeta:
                    return new AlphaBetaAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };

                case Algorithms.Random:
                    return new RandomAlgorithm<ChessRepresentation, BaseMove>(_generator);

                case Algorithms.Greedy:
                    return new GreedyAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier);

                default:
                    return new MinimaxAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier);
            }
        }

        private void ChangeAlgorithm()
        {
            Algorithms selectedAlgorithmLeft = Algorithms.Minimax;
            Algorithms selectedAlgorithmRight = Algorithms.Minimax;
            int maxDepthLeft = 2;
            int maxDepthRight = 2;

            InvokeIfRequired(listboxAlgorithmsRight, () => selectedAlgorithmRight = (Algorithms)listboxAlgorithmsRight.SelectedItem);
            InvokeIfRequired(numericMaxDepthRight, () => maxDepthRight = (int)numericMaxDepthRight.Value);
            InvokeIfRequired(listboxAlgorithmsLeft, () => selectedAlgorithmLeft = (Algorithms)listboxAlgorithmsLeft.SelectedItem);
            InvokeIfRequired(numericMaxDepthLeft, () => maxDepthLeft = (int)numericMaxDepthLeft.Value);

            _algorithmLeft = CreateAlgorithm(selectedAlgorithmLeft, maxDepthLeft);
            _algorithmRight = CreateAlgorithm(selectedAlgorithmRight, maxDepthRight);
        }

        internal enum Algorithms
        {
            Minimax,
            MinimaxAverage,
            AlphaBeta,
            Random,
            Greedy
        }

        private readonly Task taskRight;
        private readonly Task taskLeft;

        private ChessRepresentation game;
        private readonly ChessMechanism _mechanism;

        public MainForm()
        {
            game = new ChessRepresentationInitializer().Create();

            _mechanism = new ChessMechanism(true);
            _evaluator = new Evaluator(_mechanism);
            _generator = new MoveGenerator(_mechanism);
            _applier = new MoveApplier(_mechanism);

            taskRight = new Task(() =>
            {
                do
                {
                    if (!_isActive || _mechanism.GetGameState(game) != GameState.InProgress)
                    {
                        InvokeIfRequired(progressbarBotActiveRight, () =>
                        {
                            progressbarBotActiveRight.MarqueeAnimationSpeed = 0;
                        });

                        // Not doing anything, checking in every second...
                        Thread.Sleep(1000);
                        continue;
                    }

                    InvokeIfRequired(progressbarBotActiveRight, () =>
                    {
                        if (progressbarBotActiveRight.MarqueeAnimationSpeed != 10)
                        {
                            progressbarBotActiveRight.MarqueeAnimationSpeed = 10;
                        }
                    });

                    DoRobotWork(ChessPlayer.White);

                    Thread.Sleep(1000);

                } while (true);
            });

            taskLeft = new Task(() =>
            {
                do
                {
                    if (!_isActive || _mechanism.GetGameState(game) != GameState.InProgress)
                    {
                        InvokeIfRequired(progressbarBotActiveLeft, () =>
                        {
                            progressbarBotActiveLeft.MarqueeAnimationSpeed = 0;
                        });

                        // Not doing anything, checking in every second...
                        Thread.Sleep(1000);
                        continue;
                    }

                    InvokeIfRequired(progressbarBotActiveLeft, () =>
                    {
                        if (progressbarBotActiveLeft.MarqueeAnimationSpeed != 10)
                        {
                            progressbarBotActiveLeft.MarqueeAnimationSpeed = 10;
                        }
                    });

                    DoRobotWork(ChessPlayer.Black);

                    Thread.Sleep(1000);

                } while (true);
            });

            InitializeComponent();

            listboxAlgorithmsRight.Items.Add(Algorithms.Minimax);
            listboxAlgorithmsRight.Items.Add(Algorithms.MinimaxAverage);
            listboxAlgorithmsRight.Items.Add(Algorithms.AlphaBeta);
            listboxAlgorithmsRight.Items.Add(Algorithms.Greedy);
            listboxAlgorithmsRight.Items.Add(Algorithms.Random);
            listboxAlgorithmsRight.SelectedIndex = Randomizer.Next(0, listboxAlgorithmsRight.Items.Count - 1);

            listboxAlgorithmsLeft.Items.Add(Algorithms.Minimax);
            listboxAlgorithmsLeft.Items.Add(Algorithms.MinimaxAverage);
            listboxAlgorithmsLeft.Items.Add(Algorithms.AlphaBeta);
            listboxAlgorithmsLeft.Items.Add(Algorithms.Greedy);
            listboxAlgorithmsLeft.Items.Add(Algorithms.Random);
            listboxAlgorithmsLeft.SelectedIndex = Randomizer.Next(0, listboxAlgorithmsLeft.Items.Count - 1);

            chessBoardVisualizerPanel1.ChessRepresentation = game;
            chessBoardVisualizerPanel1.Refresh();

            taskLeft.Start();
            taskRight.Start();
        }

        private void StartStopAlgorithmProgressbar(ChessPlayer player, bool start)
        {
            switch (player)
            {
                case ChessPlayer.White:
                    InvokeIfRequired(progressbarAlgorithmLeft, () => progressbarAlgorithmLeft.MarqueeAnimationSpeed = start ? 10 : 0);
                    break;
                case ChessPlayer.Black:
                    InvokeIfRequired(progressbarAlgorithmRight, () => progressbarAlgorithmRight.MarqueeAnimationSpeed = start ? 10 : 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        private void DoRobotWork(ChessPlayer player)
        {
            if (game.CurrentPlayer != player)
            {
                Thread.Sleep(1000);
                return;
            }

            ChangeAlgorithm();

            var playerName = player == ChessPlayer.White ? "[WHITE]" : "[BLACK]";

            var stopWatch = new Stopwatch();
            UpdateLog($"{playerName}Running algorithm...");

            StartStopAlgorithmProgressbar(player, true);
            stopWatch.Reset();
            stopWatch.Start();
            var move = player == ChessPlayer.White 
                                    ? _algorithmLeft.Calculate(game)
                                    : _algorithmRight.Calculate(game);
            stopWatch.Stop();
            StartStopAlgorithmProgressbar(player, false);
            UpdateLog($"{playerName}Algorithm finished in {stopWatch.Elapsed.TotalSeconds:F} seconds and generated move: {move}", 2);
            game = _mechanism.ApplyMove(game, move);
            InvokeIfRequired(chessBoardVisualizerPanel1, () =>
                {
                    chessBoardVisualizerPanel1.ChessRepresentation = game;
                    chessBoardVisualizerPanel1.Refresh();
                });
            InvokeIfRequired(labelGameStatus, () => { labelGameStatus.Text = _mechanism.GetGameState(game).ToString();});
        }

        private void UpdateLog(string message, int level = 0)
        {
            var spacing = new string(' ', level);
            InvokeIfRequired(textBox1, () => textBox1.AppendText(spacing + message + Environment.NewLine));
        }

        public static void InvokeIfRequired(Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isActive = !_isActive;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            _isActive = false;
            game = new ChessRepresentationInitializer().Create();
            chessBoardVisualizerPanel1.Refresh();
        }
    }

}
