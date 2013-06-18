using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using UpboatMe.Models;

namespace UpboatMe.Utilities
{
    public class Renderer
    {
        private Meme _meme;
        private string _top;
        private string _bottom;
        private Image _image;
        private Graphics _graphics;

        public Renderer(Meme meme, string top, string bottom)
        {
            _meme = meme;
            _top = top;
            _bottom = bottom;
        }

        public byte[] Render()
        {
            using (_image = Image.FromFile(HttpContext.Current.Server.MapPath(_meme.ImagePath)))
            using (_graphics = Graphics.FromImage(_image))
            {
                DrawText(_top, true);

                DrawText(_bottom, false);

                using (var memoryStream = new MemoryStream())
                {
                    _image.Save(memoryStream, _image.RawFormat);

                    using (var streamReader = new StreamReader(memoryStream))
                    {
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private void DrawText(string text, bool isTop)
        {
            var xBuffer = 30f;
            var done = false;
            var fontSize = _meme.FontSize;

            while (!done)
            {
                int lineWidth = (int)Math.Ceiling((_image.Size.Width - xBuffer) / (fontSize / 1.5f));

                var lines = text.GetLines(lineWidth);

                var font = new Font(new FontFamily(_meme.Font), fontSize, FontStyle.Regular);

                var totalHeight = _graphics.MeasureString(text, font).Height * lines.Count;

                if (totalHeight > (isTop ? _meme.TopLineHeight : _meme.BottomLineHeight) && fontSize > 10)
                {
                    fontSize -= 2;
                    continue;
                }

                if (isTop)
                {
                    for (int x = 0; x < lines.Count; x++)
                    {
                        var size = _graphics.MeasureString(lines[x], font);

                        int centerX = (int)Math.Ceiling((_image.Size.Width / 2f) - (size.Width / 2f));

                        DrawSingleLine(_graphics, lines[x], font, Brushes.Black, Brushes.White, new PointF(centerX, x * size.Height));
                    }
                }
                else
                {
                    for (int x = 0; x < lines.Count; x++)
                    {
                        var size = _graphics.MeasureString(lines[x], font);

                        int centerX = (int)Math.Ceiling((_image.Size.Width / 2f) - (size.Width / 2f));

                        DrawSingleLine(_graphics, lines[x], font, Brushes.Black, Brushes.White, new PointF(centerX, _image.Size.Height - (size.Height * lines.Count) + (x * size.Height)));
                    }
                }

                done = true;
            }
        }

        private static void DrawSingleLine(Graphics graphics, string line, Font font, Brush stroke, Brush fill, PointF point)
        {
            using (var graphicsPath = new GraphicsPath())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                
                graphicsPath.AddString(line, font.FontFamily, (int)FontStyle.Regular, font.Size, point, StringFormat.GenericDefault);

                graphics.DrawPath(new Pen(stroke, 2), graphicsPath);
                graphics.FillPath(fill, graphicsPath);

                graphics.SmoothingMode = SmoothingMode.Default;
            }
        }
    }
}