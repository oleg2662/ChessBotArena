using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Game.Chess;
using Game.Chess.Moves;
using Game.Chess.Pieces;

namespace Tools.Common
{
    public abstract class BaseChessBoardVisualizerPanel : Panel
    {
        protected readonly StringFormat CenteredFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        protected readonly StringFormat EnPassantFormat = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Far
        };

        protected readonly StringFormat HasMovedFormat = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Far
        };

        protected BaseChessBoardVisualizerPanel()
        {
            _chessRepresentation = new ChessRepresentationInitializer().Create();
            BlackSquare = Color.SandyBrown;
            WhiteSquare = Color.BlanchedAlmond;
            Bevel = Color.Brown;
            InterpolationMode = InterpolationMode.NearestNeighbor;
            MouseMove += OnMouseMove;
            MouseClick += OnMouseClick;
        }

        public void ResetSelection()
        {
            HoverPosition = null;
            SelectedPosition = null;
            ThreatenedPositions = Enumerable.Empty<Position>();
        }

        protected virtual void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (HoverPosition == null)
            {
                ThreatenedPositions = Enumerable.Empty<Position>();
                return;
            }

            var mechanism = new ChessMechanism();
            SelectedPosition = HoverPosition;
            var selectedPositionOwner = _chessRepresentation[SelectedPosition]?.Owner;
            if (selectedPositionOwner == null)
            {
                ThreatenedPositions = Enumerable.Empty<Position>();
                return;
            }

            ThreatenedPositions = mechanism.GenerateMoves(_chessRepresentation, selectedPositionOwner).OfType<BaseChessMove>()
                .Where(x => x.From == SelectedPosition).Select(x => x.To).ToList();
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            var rect = new RectangleF(0, 0, Width, Height);
            var bevelWidth = 0.05f * rect.Width;
            var bevelHeight = 0.05f * rect.Height;

            var innerBounds = new RectangleF(rect.X + bevelWidth,
                rect.Y + bevelHeight,
                rect.Width - 2 * bevelWidth,
                rect.Height - 2 * bevelHeight);

            var cellHeight = innerBounds.Height / 8.0d;
            var cellWidth = innerBounds.Width / 8.0d;

            var x = (int)Math.Round(e.X - bevelWidth);
            var y = (int)Math.Round(e.Y - bevelHeight);

            var col = x <= 0 || x >= innerBounds.Width ? null : (int?)Math.Floor(x / cellWidth);
            var row = y <= 0 || y >= innerBounds.Height ? null : (int?)Math.Floor(y / cellHeight);

            if (row.HasValue && col.HasValue)
            {
                HoverPosition = (Position)(row * 8 + col);
            }
            else
            {
                HoverPosition = null;
            }

            Refresh();
        }

        public Position HoverPosition { get; private set; }

        public Position SelectedPosition { get; private set; }

        protected IEnumerable<Position> ThreatenedPositions { get; private set; } = Enumerable.Empty<Position>();

        private InterpolationMode _interpolationMode;
        public InterpolationMode InterpolationMode
        {
            get => _interpolationMode;
            set
            {
                _interpolationMode = value;
                Refresh();
            }
        }

        private ChessRepresentation _chessRepresentation;
        public ChessRepresentation ChessRepresentation
        {
            private get => _chessRepresentation;
            set
            {
                _chessRepresentation = value;
                ResetSelection();
                Refresh();
            }
        }

        private Color _blackSquare;
        public Color BlackSquare
        {
            get => _blackSquare;
            set
            {
                _blackSquare = value;
                BlackSquareBrush = new SolidBrush(_blackSquare);
                Refresh();
            }
        }

        private Color _whiteSquare;
        public Color WhiteSquare
        {
            get => _whiteSquare;
            set
            {
                _whiteSquare = value;
                WhiteSquareBrush = new SolidBrush(_whiteSquare);
                Refresh();
            }
        }

        private Color _bevel;
        public Color Bevel
        {
            get => _bevel;
            set
            {
                _bevel = value;
                BevelBrush = new SolidBrush(_bevel);
                Refresh();
            }
        }

        public Brush BlackSquareBrush
        {
            get;
            private set;
        }

        public Brush WhiteSquareBrush
        {
            get;
            private set;
        }

        public Brush BevelBrush
        {
            get;
            private set;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var image = GenerateChessBoardContent(Rectangle.Round(pe.Graphics.VisibleClipBounds).Size);
            pe.Graphics.InterpolationMode = InterpolationMode;
            var g = pe.Graphics;
            g.DrawImage(image, g.VisibleClipBounds);

            foreach (var p in ThreatenedPositions)
            {
                var color = _chessRepresentation[SelectedPosition].Owner == _chessRepresentation.CurrentPlayer
                    ? Color.LawnGreen
                    : Color.OrangeRed;

                HighlightField(g, p, color);
            }

            HighlightField(g, SelectedPosition, Color.Blue);

            HighlightField(g, HoverPosition, Color.Yellow);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            Refresh();
        }

        protected void HighlightField(Graphics g, Position p, Color color)
        {
            if (p == null)
            {
                return;
            }

            var rect = new RectangleF(0, 0, Width, Height);
            var bevelWidth = 0.05f * rect.Width;
            var bevelHeight = 0.05f * rect.Height;

            var innerBounds = new RectangleF(rect.X + bevelWidth,
                rect.Y + bevelHeight,
                rect.Width - 2 * bevelWidth,
                rect.Height - 2 * bevelHeight);

            var cellHeight = innerBounds.Height / 8.0f;
            var cellWidth = innerBounds.Width / 8.0f;

            var index = (int)p;

            var row = index / 8;
            var col = index % 8;

            var pen = new Pen(color, 5);

            g.DrawRectangle(pen, bevelWidth + col * cellWidth,
                bevelHeight + row * cellHeight,
                cellWidth,
                cellHeight);
        }

        protected virtual Image GenerateChessBoardContent(Size outputSize)
        {
            var rect = new Rectangle(new Point(0, 0), outputSize);

            var bevelWidth = 0.05f * rect.Width;
            var bevelHeight = 0.05f * rect.Height;

            var innerBounds = new RectangleF(rect.X + bevelWidth,
                rect.Y + bevelHeight,
                rect.Width - 2 * bevelWidth,
                rect.Height - 2 * bevelHeight);

            var bitmap = new Bitmap(rect.Width, rect.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(BevelBrush, rect);
                g.FillRectangle(WhiteSquareBrush, innerBounds);

                // Bevel characters
                DrawRowNumbers(g, new RectangleF(0, innerBounds.Y, bevelWidth, innerBounds.Height));
                DrawRowNumbers(g, new RectangleF(innerBounds.Right, innerBounds.Y, bevelWidth, innerBounds.Height));
                DrawColumnChars(g, new RectangleF(innerBounds.X, 0, innerBounds.Width, bevelHeight));
                DrawColumnChars(g, new RectangleF(innerBounds.X, innerBounds.Bottom, innerBounds.Width, bevelHeight));

                // Board and pieces
                for (var i = 0; i < 64; i++)
                {
                    var p = (Position)i;

                    var brush = p.BlackField ? BlackSquareBrush : WhiteSquareBrush;
                    var rectangle = GetFieldRectangle(innerBounds, (Position)i);

                    g.FillRectangle(brush, rectangle);

                    var figureText = ChessRepresentation?[p]?.ToString() ?? string.Empty;
                    var hasMoved = ChessRepresentation?[p]?.HasMoved ?? false;
                    var enPassant = (ChessRepresentation?[p] as Pawn)?.IsEnPassantCapturable ?? false;

                    g.DrawString(
                        figureText,
                        new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                        Brushes.Black,
                        rectangle,
                        CenteredFormat);

                    if (enPassant)
                    {
                        g.DrawString(
                            "ep.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            EnPassantFormat);
                    }

                    if (hasMoved)
                    {
                        g.DrawString(
                            "mvd.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            HasMovedFormat);
                    }
                }
            }

            return bitmap;
        }

        protected virtual void DrawRowNumbers(Graphics g, RectangleF rect)
        {
            var cellHeight = rect.Height / 8;
            var cellWidth = rect.Width;

            // Bevel characters
            for (var i = 0; i < 8; i++)
            {
                var rectangle = new RectangleF(rect.X, rect.Y + i * cellHeight, cellWidth, cellHeight);
                g.DrawString(
                    $"{8 - i}",
                    new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                    Brushes.White,
                    rectangle,
                    CenteredFormat);
            }
        }

        protected virtual void DrawColumnChars(Graphics g, RectangleF rect)
        {
            var cellHeight = rect.Height;
            var cellWidth = rect.Width / 8;

            // Bevel characters
            for (var i = 0; i < 8; i++)
            {
                var rectangle = new RectangleF(rect.X + i * cellWidth, rect.Y, cellWidth, cellHeight);
                g.DrawString(
                    $"{(char)('A' + i)}",
                    new Font(FontFamily.GenericSansSerif, rectangle.Height / 2, FontStyle.Regular),
                    Brushes.White,
                    rectangle,
                    CenteredFormat);
            }
        }

        protected RectangleF GetFieldRectangle(RectangleF clipRectangle, Position position)
        {
            var col = position.Column - 'A';
            var row = 8 - position.Row;

            var width = clipRectangle.Width / 8.0f;
            var height = clipRectangle.Height / 8.0f;

            var calculatedPosition = new PointF(clipRectangle.Left + col * width, clipRectangle.Top + row * height);
            var calculatedSize = new SizeF(width, height);

            return new RectangleF(calculatedPosition, calculatedSize);
        }
    }
}