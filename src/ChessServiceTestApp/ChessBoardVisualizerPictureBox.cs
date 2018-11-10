using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Chess;
using Game.Chess.Pieces;

namespace ChessServiceTestApp
{
    public sealed class ChessBoardVisualizerPictureBox : Panel
    {
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

        public Color BlackSquare { get; set; }

        public Color WhiteSquare { get; set; }

        public Color Bevel { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var image = PaintChessBoard();
            pe.Graphics.InterpolationMode = InterpolationMode;
            var g = pe.Graphics;
            g.DrawImage(image, g.VisibleClipBounds);
            g.Dispose();
        }

        private Image PaintChessBoard()
        {
            var bitmap = new Bitmap(1000, 1000);

            using (var g = Graphics.FromImage(bitmap))
            {
                var blackBrush = new SolidBrush(BlackSquare);
                var whiteBrush = new SolidBrush(WhiteSquare);
                var bevelBrush = new SolidBrush(Bevel);
                var bevelWidth = 0.05f * g.VisibleClipBounds.Width;
                var bevelHeight = 0.05f * g.VisibleClipBounds.Height;

                var innerBounds = new RectangleF(g.VisibleClipBounds.X + bevelWidth,
                    g.VisibleClipBounds.Y + bevelHeight,
                    g.VisibleClipBounds.Width - 2 * bevelWidth,
                    g.VisibleClipBounds.Height - 2 * bevelHeight);

                g.FillRectangle(bevelBrush, g.VisibleClipBounds);
                g.FillRectangle(whiteBrush, innerBounds);
                
                // Bevel characters
                DrawRowNumbers(g, new RectangleF(0, innerBounds.Y, bevelWidth, innerBounds.Height));
                DrawRowNumbers(g, new RectangleF(innerBounds.Right, innerBounds.Y, bevelWidth, innerBounds.Height));
                DrawColumnChars(g, new RectangleF(innerBounds.X, 0, innerBounds.Width, bevelHeight));
                DrawColumnChars(g, new RectangleF(innerBounds.X, innerBounds.Bottom, innerBounds.Width, bevelHeight));

                // Board and pieces
                for (var i = 0; i < 64; i++)
                {
                    Position p = (Position)i;

                    var brush = p.BlackField ? blackBrush : whiteBrush;
                    var rectangle = GetFieldRectangle(innerBounds, (Position)i);

                    g.FillRectangle(brush, rectangle);

                    var figureText = ChessRepresentation?[p]?.ToString() ?? string.Empty;
                    var hasMoved = ChessRepresentation?[p]?.HasMoved ?? false;
                    var enPassant = (ChessRepresentation?[p] as Pawn)?.IsEnPassantCapturable ?? false;

                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    var sfEnPassant = new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Far
                    };

                    var sfHasMoved = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Far
                    };

                    g.DrawString(
                        figureText,
                        new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                        Brushes.Black,
                        rectangle,
                        sf);

                    if (enPassant)
                    {
                        g.DrawString(
                            "ep.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            sfEnPassant);
                    }

                    if (hasMoved)
                    {
                        g.DrawString(
                            "mvd.",
                            new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
                            Brushes.Black,
                            rectangle,
                            sfHasMoved);
                    }
                }
            }

            return bitmap;
        }

        StringFormat sfCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private void DrawRowNumbers(Graphics g, RectangleF rect)
        {
            var cellHeight = rect.Height / 8;
            var cellWidth = rect.Width;

            // Bevel characters
            for (var i = 0; i < 8; i++)
            {
                var rectangle = new RectangleF(rect.X, rect.Y + i * cellHeight, cellWidth, cellHeight);
                g.DrawString(
                    $"{i+1}",
                    new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                    Brushes.White,
                    rectangle,
                    sfCenter);
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
                    sfCenter);
            }
        }


        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    Image = PaintChessBoard();
        //    //using (var g = pe.Graphics)
        //    //{
        //    //    g.im
        //    //}
        //    ////var bitmap = new Bitmap(1000, 1000);
        //    ////Image = bitmap;
        //    //using (var g = pe.Graphics)
        //    //{
        //    //    var blackBrush = new SolidBrush(BlackSquare);
        //    //    var whiteBrush = new SolidBrush(WhiteSquare);
        //    //    var bevelBrush = new SolidBrush(Bevel);
        //    //    var bevelWidth = 0.05f * g.VisibleClipBounds.Width;
        //    //    var bevelHeight = 0.05f * g.VisibleClipBounds.Height;

        //    //    var innerBounds = new RectangleF(g.VisibleClipBounds.X + bevelWidth,
        //    //        g.VisibleClipBounds.Y + bevelHeight,
        //    //        g.VisibleClipBounds.Width - 2 * bevelWidth,
        //    //        g.VisibleClipBounds.Height - 2 * bevelHeight);

        //    //    g.FillRectangle(bevelBrush, g.VisibleClipBounds);
        //    //    g.FillRectangle(whiteBrush, innerBounds);

        //    //    for (var i = 0; i < 64; i++)
        //    //    {
        //    //        Position p = (Position) i;

        //    //        var brush = p.BlackField ? blackBrush : whiteBrush;
        //    //        var rectangle = GetFieldRectangle(innerBounds, (Position) i);

        //    //        g.FillRectangle(brush, rectangle);

        //    //        var figureText = ChessRepresentation?[p]?.ToString() ?? string.Empty;
        //    //        var hasMoved = ChessRepresentation?[p]?.HasMoved ?? false;
        //    //        var enPassant = (ChessRepresentation?[p] as Pawn)?.IsEnPassantCapturable ?? false;

        //    //        var sf = new StringFormat
        //    //        {
        //    //            Alignment = StringAlignment.Center,
        //    //            LineAlignment = StringAlignment.Center
        //    //        };

        //    //        var sfEnPassant = new StringFormat
        //    //        {
        //    //            Alignment = StringAlignment.Far,
        //    //            LineAlignment = StringAlignment.Far
        //    //        };

        //    //        var sfHasMoved = new StringFormat
        //    //        {
        //    //            Alignment = StringAlignment.Near,
        //    //            LineAlignment = StringAlignment.Far
        //    //        };

        //    //        g.DrawString(
        //    //            figureText,
        //    //            new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
        //    //            Brushes.Black,
        //    //            rectangle,
        //    //            sf);

        //    //        if (enPassant)
        //    //        {
        //    //            g.DrawString(
        //    //                "ep.",
        //    //                new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
        //    //                Brushes.Black,
        //    //                rectangle,
        //    //                sfEnPassant);
        //    //        }

        //    //        if (hasMoved)
        //    //        {
        //    //            g.DrawString(
        //    //                "mvd.",
        //    //                new Font(FontFamily.GenericSansSerif, rectangle.Width / 8, FontStyle.Regular),
        //    //                Brushes.Black,
        //    //                rectangle,
        //    //                sfHasMoved);
        //    //        }
        //    //    }
        //    //}
        //}

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
