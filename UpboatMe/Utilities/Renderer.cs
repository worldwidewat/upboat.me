using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using UpboatMe.Models;

namespace UpboatMe.Utilities
{
    public class Renderer
    {
        public byte[] Render(Meme _meme, string top, string bottom, bool drawBoxes)
        {
            using (var image = Image.FromFile(HttpContext.Current.Server.MapPath(_meme.ImagePath)))
            using (var graphics = Graphics.FromImage(image))
            {
                DrawText(graphics, image, _meme, top, true, drawBoxes);

                DrawText(graphics, image, _meme, bottom, false, drawBoxes);

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, image.RawFormat);

                    return memoryStream.ToArray();
                }
            }
        }

        private void DrawText(Graphics graphics, Image image, Meme meme, string text, bool isTop, bool drawBoxes)
        {
            var done = false;
            var fontSize = meme.FontSize;
            var maxHeightPercent = isTop ? meme.TopLineHeightPercent : meme.BottomLineHeightPercent;
            var maxHeight = (int)Math.Ceiling(image.Height * (maxHeightPercent / 100 ));
            var stringFormat = new StringFormat(StringFormat.GenericTypographic);

            stringFormat.Alignment = StringAlignment.Center;

            while (!done)
            {
                var font = new Font(new FontFamily(meme.Font), fontSize, FontStyle.Regular);

                var size = graphics.MeasureString(text, font, image.Width);

                if (size.Height > maxHeight && fontSize > 10)
                {
                    fontSize -= 2;

                    continue;
                }

                var bounds = new Rectangle(0, 0, image.Width, (int)size.Height);

                if (!isTop)
                {
                    bounds.Y = image.Height - bounds.Height - 1;
                    stringFormat.LineAlignment = StringAlignment.Far;
                }

                using (var graphicsPath = new GraphicsPath())
                {
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    float emSize = graphics.DpiY * fontSize / 72;

                    graphicsPath.AddString(text, font.FontFamily, (int)FontStyle.Regular, emSize, bounds, stringFormat);

                    graphics.DrawPath(new Pen(meme.Stroke, 2) { LineJoin = LineJoin.Round }, graphicsPath);
                    graphics.FillPath(meme.Fill, graphicsPath);

                    graphics.SmoothingMode = SmoothingMode.Default;
                }

                if (drawBoxes)
                {
                    DrawBoxes(graphics, image.Width, image.Height, maxHeight, isTop);
                }

                done = true;
            }
        }

        private void DrawBoxes(Graphics graphics, int width, int height, int maxHeight, bool isTop)
        {
            graphics.CompositingMode = CompositingMode.SourceOver;

            var brush = new SolidBrush(Color.FromArgb(150, Color.Red));

            graphics.FillRectangle(brush, 0, isTop ? 0 : height - maxHeight, width, maxHeight);

            for (int x = 0; x < height; x += 20)
            {
                graphics.DrawString(x.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, x);
            }

            graphics.DrawString(string.Format("H: {0}, W: {1}", height, width), SystemFonts.DefaultFont, Brushes.Black, width / 2, 20);

            graphics.CompositingMode = CompositingMode.SourceCopy;
        }
    }
}