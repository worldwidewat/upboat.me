using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public class SpriteGenerator :GeneratorBase
    {
        public SpriteGenerator(SpriteThumbsConfiguration configuration)
            : base(configuration)
        {
        }

        public void Generate()
        {
            var files = Directory.GetFiles(Configuration.ThumbImagesPath, "*.jpg");

            if (!files.Any())
            {
                return;
            }

            var width = files.Length < Configuration.ThumbsPerRow ? files.Length : Configuration.ThumbsPerRow;
            var height = (int)Math.Ceiling(files.Length / (double)Configuration.ThumbsPerRow);

            if (File.Exists(Configuration.SpriteFullFileName))
            {
                File.Delete(Configuration.SpriteFullFileName);
            }

            if (File.Exists(Configuration.StylesheetFilePath))
            {
                File.Delete(Configuration.StylesheetFilePath);
            }

            using (var sprite = new Bitmap(width * Configuration.ThumbWidth, height * Configuration.ThumbHeight))
            using (var graphics = Graphics.FromImage(sprite))
            using (var stream = File.Open(Configuration.StylesheetFilePath, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(".thumb {{ width: {0}px; height: {1}px; background-image: url({2}); background-position: {0}px {1}px; }}", Configuration.ThumbWidth, Configuration.ThumbHeight, SpriteThumbsConfiguration.SpriteResource);

                for (int x = 0; x < files.Length; x++)
                {
                    var columnIndex = x % Configuration.ThumbsPerRow;
                    var rowIndex = (int)Math.Floor(x / (double)Configuration.ThumbsPerRow);

                    var left = columnIndex * Configuration.ThumbWidth;
                    var top = rowIndex * Configuration.ThumbHeight;

                    WriteFileToSprite(files[x], graphics, left, top);
                    WriteFileCssInfo(Path.GetFileNameWithoutExtension(files[x]), writer, left, top);
                }

                SaveImage(sprite, Configuration.SpriteFullFileName);
            }
        }

        private void WriteFileToSprite(string file, Graphics spriteGraphics, int left, int top)
        {
            using (var image = new Bitmap(file))
            {
                var bounds = new Rectangle(left, top, Configuration.ThumbWidth, Configuration.ThumbHeight);

                spriteGraphics.DrawImage(image, bounds);
            }
        }

        private void WriteFileCssInfo(string fileNameWithoutExtension, StreamWriter writer, int left, int top)
        {
            var entry = string.Format(".bg-{0} {{ background-position: -{1}px -{2}px; }}", fileNameWithoutExtension, left, top);

            writer.WriteLine(entry);
        }
    }
}
