using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Game.Chess;
using Game.Chess.Pieces;

namespace ChessServiceTestApp
{
    public sealed class ChessBoardVisualizerPictureBox : Panel
    {
        private readonly StringFormat _sfCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private readonly StringFormat _sfEnPassant = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Far
        };
        private readonly StringFormat _sfHasMoved = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Far
        };

        public ChessBoardVisualizerPictureBox()
        {
            _chessRepresentation = new ChessRepresentationInitializer().Create();
            BlackSquare = Color.SandyBrown;
            WhiteSquare = Color.BlanchedAlmond;
            Bevel = Color.Brown;
            DoubleBuffered = true;
            InterpolationMode = InterpolationMode.NearestNeighbor;
        }

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
        }

        protected override void OnResize(EventArgs eventargs)
        {
            Refresh();
        }

        private Image GenerateChessBoardContent(Size outputSize)
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
                        _sfCenter);

                    if (enPassant)
                    {
                        g.DrawString(
                            "ep.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            _sfEnPassant);
                    }

                    if (hasMoved)
                    {
                        g.DrawString(
                            "mvd.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            _sfHasMoved);
                    }
                }
            }

            return bitmap;
        }

        private void DrawRowNumbers(Graphics g, RectangleF rect)
        {
            var cellHeight = rect.Height / 8;
            var cellWidth = rect.Width;

            // Bevel characters
            for (var i = 0; i < 8; i++)
            {
                var rectangle = new RectangleF(rect.X, rect.Y + i * cellHeight, cellWidth, cellHeight);
                g.DrawString(
                    $"{8-i}",
                    new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                    Brushes.White,
                    rectangle,
                    _sfCenter);
            }
        }

        private void DrawColumnChars(Graphics g, RectangleF rect)
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
                    _sfCenter);
            }
        }

        private RectangleF GetFieldRectangle(RectangleF clipRectangle, Position position)
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
