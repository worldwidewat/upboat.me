using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public class SpriteGenerator : GeneratorBase
    {
        public SpriteGenerator(SpriteThumbsConfiguration configuration)
            : base(configuration)
        {
        }

        public void Generate()
        {
            var resourceHash = Configuration.GetResourceHash();

            if (File.Exists(Configuration.FullHashFileName))
            {
                var hash = File.ReadAllText(Configuration.FullHashFileName);

                // If the previous hash is the same as the current, and we already have sprite and css files, don't regenerate
                if (string.Equals(hash, resourceHash, StringComparison.Ordinal) &&
                    File.Exists(Configuration.SpriteFullFileName) &&
                    File.Exists(Configuration.StylesheetFullFileName))
                {
                    return;
                }
            }

            if (!Configuration.RawImages.Any())
            {
                return;
            }

            var width = Configuration.RawImages.Count < Configuration.ThumbsPerRow ? Configuration.RawImages.Count : Configuration.ThumbsPerRow;
            var height = (int)Math.Ceiling(Configuration.RawImages.Count / (double)Configuration.ThumbsPerRow);

            if (File.Exists(Configuration.SpriteFullFileName))
            {
                File.Delete(Configuration.SpriteFullFileName);
            }

            if (File.Exists(Configuration.StylesheetFullFileName))
            {
                File.Delete(Configuration.StylesheetFullFileName);
            }

            using (var sprite = new Bitmap(width * Configuration.ThumbWidth, height * Configuration.ThumbHeight))
            using (var graphics = Graphics.FromImage(sprite))
            using (var stream = File.Open(Configuration.StylesheetFullFileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(".{0} {{ width: {1}px; height: {2}px; background-image: url({3}); background-position: {1}px {2}px; }}",
                    SpriteThumbsConfiguration.GetThumbClassName(), Configuration.ThumbWidth, Configuration.ThumbHeight, SpriteThumbsConfiguration.SpriteResource + "&hash=" + resourceHash);

                for (int x = 0; x < Configuration.RawImages.Count; x++)
                {
                    var columnIndex = x % Configuration.ThumbsPerRow;
                    var rowIndex = (int)Math.Floor(x / (double)Configuration.ThumbsPerRow);

                    var left = columnIndex * Configuration.ThumbWidth;
                    var top = rowIndex * Configuration.ThumbHeight;

                    WriteFileToSprite(Configuration.RawImages[x], graphics, left, top);
                    WriteFileCssInfo(Configuration.RawImages[x], writer, left, top);
                }

                SaveImage(sprite, Configuration.SpriteFullFileName);
            }

            // Store an updated hash so we don't re-generate resources unnecessarily the next time the app starts up
            File.WriteAllText(Configuration.FullHashFileName, resourceHash);
        }

        private void WriteFileToSprite(RawImage rawImage, Graphics spriteGraphics, int left, int top)
        {
            using (var image = new Bitmap(rawImage.FullFilePath))
            {
                var isPortrait = image.Width < image.Height;

                Rectangle crop;

                if (isPortrait)
                {
                    var y = (int)((image.Height / 2d) - (image.Width / 2d));

                    crop = new Rectangle(0, y, image.Width, image.Width);
                }
                else
                {
                    var x = (int)((image.Width / 2d) - (image.Height / 2d));

                    crop = new Rectangle(x, 0, image.Height, image.Height);
                }

                spriteGraphics.DrawImage(image, new Rectangle(left, top, Configuration.ThumbWidth, Configuration.ThumbHeight), crop, GraphicsUnit.Pixel);
            }
        }

        private void WriteFileCssInfo(RawImage rawImage, StreamWriter writer, int left, int top)
        {
            var className = SpriteThumbsConfiguration.GetImageClassName(rawImage.Id);
            var entry = string.Format(".{0} {{ background-position: -{1}px -{2}px; }}", className, left, top);

            writer.WriteLine(entry);
        }
    }
}
