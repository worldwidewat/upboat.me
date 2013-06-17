using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        public ActionResult Make(string meme, string first, string last)
        {
            var image = Image.FromFile(Server.MapPath("~/Images/success-kid-template.png"));

            DrawText(image, first, 40, 200, false);

            DrawText(image, last, 40, 150, true);

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);

                using (var streamReader = new StreamReader(memoryStream))
                {
                    return new FileContentResult(memoryStream.ToArray(), "image/png"); ;
                }
            }
        }

        private void DrawText(Image image, string text, int fontSize, int maxHeight, bool isBottom)
        {
            var graphics = Graphics.FromImage(image);
            var xBuffer = 30f;
            var done = false;

            while (!done)
            {
                int lineWidth = (int)Math.Ceiling((image.Size.Width - xBuffer) / (fontSize / 1.5f));

                var lines = GetLines(text, lineWidth);

                var font = new Font(new FontFamily("Impact"), fontSize, FontStyle.Regular);

                var totalHeight = graphics.MeasureString(text, font).Height * lines.Count;

                if (totalHeight > maxHeight && font.Size > 10)
                {
                    fontSize -= 2;
                    continue;
                }

                if (!isBottom)
                {
                    for (int x = 0; x < lines.Count; x++)
                    {
                        var size = graphics.MeasureString(lines[x], font);

                        int centerX = (int)Math.Ceiling((image.Size.Width / 2f) - (size.Width / 2f));

                        DrawSingleLine(graphics, lines[x], font, Brushes.Black, Brushes.White, new PointF(centerX, x * size.Height));
                    }
                }
                else
                {
                    for (int x = 0; x < lines.Count; x++)
                    {
                        var size = graphics.MeasureString(lines[x], font);

                        int centerX = (int)Math.Ceiling((image.Size.Width / 2f) - (size.Width / 2f));

                        DrawSingleLine(graphics, lines[x], font, Brushes.Black, Brushes.White, new PointF(centerX, image.Size.Height - (size.Height * lines.Count) + (x * size.Height)));
                    }
                }

                done = true;
            }
        }

        private static void DrawSingleLine(Graphics graphics, string line, Font font, Brush stroke, Brush fill, PointF point)
        {
            const int offset = 1;

            graphics.DrawString(line, font, stroke, new PointF(point.X - offset, point.Y - offset));
            graphics.DrawString(line, font, stroke, new PointF(point.X + offset, point.Y - offset));
            graphics.DrawString(line, font, stroke, new PointF(point.X - offset, point.Y + offset));
            graphics.DrawString(line, font, stroke, new PointF(point.X + offset, point.Y + offset));

            graphics.DrawString(line, font, fill, point);
        }

        private static List<string> GetLines(string text, int margin)
        {
            int start = 0, end;
            var lines = new List<string>();

            while ((end = start + margin) < text.Length)
            {
                while (text[end] != ' ' && end > start)
                {
                    end -= 1;
                }

                if (end == start)
                {
                    end = start + margin;
                }

                lines.Add(text.Substring(start, end - start));
                start = end + 1;
            }

            if (start < text.Length)
            {
                lines.Add(text.Substring(start));
            }

            return lines;
        }
    }
}
