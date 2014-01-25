using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UpboatMe.Imaging
{
    public class Renderer
    {
        public byte[] Render(RenderParameters parameters)
        {
            using (var image = Image.FromFile(parameters.FullImagePath))
            using (var graphics = Graphics.FromImage(image))
            {
                DrawWatermark(parameters, graphics, image);

                foreach (var line in parameters.Lines)
                {
                    var maxHeightPercent = line.HeightPercent;
                    var maxHeight = (int)Math.Ceiling(image.Height * (maxHeightPercent / 100));
                    var bounds = line.Bounds ?? new Rectangle(0, 0, image.Width, maxHeight);
                    var stringFormat = new StringFormat(StringFormat.GenericTypographic);

                    stringFormat.Alignment = line.TextAlignment;
                    stringFormat.LineAlignment = StringAlignment.Near;

                    if (line.HugBottom)
                    {
                        stringFormat.LineAlignment = StringAlignment.Far;
                        bounds.Y = image.Height - bounds.Height - 1;
                    }
                    
                    DrawText(parameters, graphics, image, line, bounds, stringFormat);
                }
                  
                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, image.RawFormat);

                    return memoryStream.ToArray();
                }
            }
        }

        private void DrawWatermark(RenderParameters parameters, Graphics graphics, Image image)
        {
            using (var watermark = Image.FromFile(parameters.FullWatermarkImageFilePath))
            {
                var padding = 2;
                var width = parameters.WatermarkImageWidth;
                var height = parameters.WatermarkImageHeight;
                var sourceRectangle = new Rectangle(0, 0, watermark.Width, watermark.Height);
                var destinationRectangle = new Rectangle(image.Width - width - padding, image.Height - height - padding, width, height);

                graphics.DrawImage(watermark, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);

                var font = new Font(parameters.WatermarkFont, parameters.WatermarkFontSize);

                var textSize = graphics.MeasureString(parameters.WatermarkText, font);

                var bounds = new Rectangle(image.Width - width - (int)Math.Ceiling(textSize.Width), image.Height - (int)Math.Ceiling(textSize.Height), image.Width, (int)Math.Ceiling(textSize.Height));

                graphics.CompositingMode = CompositingMode.SourceOver;

                var stroke = new SolidBrush(Color.FromArgb(150, parameters.WatermarkStroke));
                var fill = new SolidBrush(Color.FromArgb(150, parameters.WatermarkFill));
                
                DrawText(graphics, parameters.WatermarkText, font, parameters.WatermarkFontStyle, parameters.WatermarkFontSize, stroke, parameters.WatermarkStrokeWidth, fill, StringFormat.GenericTypographic, bounds);

                graphics.CompositingMode = CompositingMode.SourceCopy;
            }
        }

        private void DrawText(RenderParameters parameters, Graphics graphics, Image image, LineParameters line, Rectangle bounds, StringFormat stringFormat)
        {
            var done = false;
            var fontSize = line.FontSize;
            var fontFamily = FindFont(parameters, line.Font);

            while (!done)
            {
                var font = new Font(fontFamily, fontSize, FontStyle.Regular);

                var size = graphics.MeasureString(line.Text, font, bounds.Width);
                
                if (size.Height > bounds.Size.Height && fontSize > 10)
                {
                    fontSize -= 2;
                    continue;
                }

                var stroke = new SolidBrush(line.Stroke);
                var fill = new SolidBrush(line.Fill);

                DrawText(graphics, line.Text, font, line.FontStyle, fontSize, stroke, line.StrokeWidth, fill, stringFormat, bounds);

                if (parameters.DebugMode)
                {
                    DrawBoxes(graphics, image.Width, image.Height, bounds);
                }

                done = true;
            }
        }

        private static FontFamily FindFont(RenderParameters parameters, string font)
        {
            var fontFamily = parameters.PrivateFonts.Families.FirstOrDefault(f => f.Name == font)
                             ?? FontFamily.Families.FirstOrDefault(f => f.Name == font);

            if (fontFamily == null)
            {
                throw new ArgumentException(string.Format("Font {0} could not be found", font));
            }

            return fontFamily;
        }

        private static void DrawText(Graphics graphics, string text, Font font, FontStyle fontStyle, int fontSize, Brush stroke, int strokeWidth, Brush fill, StringFormat stringFormat, Rectangle bounds)
        {
            using (var graphicsPath = new GraphicsPath())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                float emSize = graphics.DpiY * fontSize / 72;

                if (strokeWidth >= 0)
                {
                    graphicsPath.AddString(text, font.FontFamily, (int) fontStyle, emSize, bounds, stringFormat);
                    graphics.DrawPath(new Pen(stroke, strokeWidth) {LineJoin = LineJoin.Round}, graphicsPath);
                    graphics.FillPath(fill, graphicsPath);
                }
                else
                {
                    font = new Font(font, fontStyle);
                    graphics.CompositingMode = CompositingMode.SourceOver;
                    graphics.DrawString(text, font, fill, bounds, stringFormat);
                }
                graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        private static void DrawBoxes(Graphics graphics, int width, int height, Rectangle bounds)
        {
            graphics.CompositingMode = CompositingMode.SourceOver;

            var brush = new SolidBrush(Color.FromArgb(150, Color.Red));

            graphics.FillRectangle(brush, bounds);

            for (int x = 0; x < height; x += 20)
            {
                graphics.DrawString(x.ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont, Brushes.Black, 0, x);
            }

            graphics.DrawString(string.Format("H: {0}, W: {1}", height, width), SystemFonts.DefaultFont, Brushes.Black, width / 2f, 20);

            graphics.CompositingMode = CompositingMode.SourceCopy;
        }
    }
}