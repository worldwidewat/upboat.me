using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace UpboatMe.Imaging
{
    public class Renderer
    {
        public byte[] Render(RenderParameters parameters, string top, string bottom)
        {
            using (var image = Image.FromFile(parameters.FullImagePath))
            using (var graphics = Graphics.FromImage(image))
            {
                DrawWatermark(parameters, graphics, image);

                DrawText(parameters, graphics, image, top, true);

                DrawText(parameters, graphics, image, bottom, false);

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

                var textX = watermark.Width - width - (int)Math.Ceiling(textSize.Width);

                var bounds = new Rectangle(image.Width - width - (int)Math.Ceiling(textSize.Width), image.Height - (int)Math.Ceiling(textSize.Height), image.Width, (int)Math.Ceiling(textSize.Height));

                graphics.CompositingMode = CompositingMode.SourceOver;

                var stroke = new SolidBrush(Color.FromArgb(150, parameters.WatermarkStroke));
                var fill = new SolidBrush(Color.FromArgb(150, parameters.WatermarkFill));

                DrawText(graphics, parameters.WatermarkText, font, parameters.WatermarkFontSize, stroke, parameters.WatermarkStrokeWidth, fill, StringFormat.GenericTypographic, bounds);

                graphics.CompositingMode = CompositingMode.SourceCopy;
            }
        }

        private void DrawText(RenderParameters parameters, Graphics graphics, Image image, string text, bool isTop)
        {
            var done = false;
            var fontSize = parameters.FontSize;
            var maxHeightPercent = isTop ? parameters.TopLineHeightPercent : parameters.BottomLineHeightPercent;
            var maxHeight = (int)Math.Ceiling(image.Height * (maxHeightPercent / 100));
            var stringFormat = new StringFormat(StringFormat.GenericTypographic);

            var bounds = (isTop ? parameters.TopLineBounds : parameters.BottomLineBounds)
                                    ?? new Rectangle(0, 0, image.Width, maxHeight);

            
            stringFormat.Alignment = StringAlignment.Center;

            while (!done)
            {
                var font = new Font(new FontFamily(parameters.Font), fontSize, FontStyle.Regular);

                var size = graphics.MeasureString(text, font, bounds.Width);
                
                if (size.Height > bounds.Size.Height && fontSize > 10)
                {
                    fontSize -= 2;
                    continue;
                }


                if (!isTop && parameters.BottomLineBounds == null)
                {
                    bounds.Y = image.Height - bounds.Height - 1;
                    stringFormat.LineAlignment = StringAlignment.Far;
                }

                var stroke = new SolidBrush(parameters.Stroke);
                var fill = new SolidBrush(parameters.Fill);

                DrawText(graphics, text, font, fontSize, stroke, parameters.StrokeWidth, fill, stringFormat, bounds);

                if (parameters.DebugMode)
                {
                    DrawBoxes(graphics, image.Width, image.Height, bounds);
                }

                done = true;
            }
        }

        private static void DrawText(Graphics graphics, string text, Font font, int fontSize, Brush stroke, int strokeWidth, Brush fill, StringFormat stringFormat, Rectangle bounds)
        {
            using (var graphicsPath = new GraphicsPath())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                float emSize = graphics.DpiY * fontSize / 72;

                graphicsPath.AddString(text, font.FontFamily, (int)FontStyle.Regular, emSize, bounds, stringFormat);

                graphics.DrawPath(new Pen(stroke, strokeWidth) { LineJoin = LineJoin.Round }, graphicsPath);
                graphics.FillPath(fill, graphicsPath);

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
                graphics.DrawString(x.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, x);
            }

            graphics.DrawString(string.Format("H: {0}, W: {1}", height, width), SystemFonts.DefaultFont, Brushes.Black, width / 2, 20);

            graphics.CompositingMode = CompositingMode.SourceCopy;
        }
    }
}