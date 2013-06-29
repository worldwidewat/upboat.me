using System.Drawing;
using System.IO;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public class ThumbsGenerator : GeneratorBase
    {
        public ThumbsGenerator(SpriteThumbsConfiguration configuration)
            : base(configuration)
        {
        }

        public void Generate()
        {
            var files = Directory.GetFiles(Configuration.RawImagesPath, "*.jpg");

            if (!files.Any())
            {
                return;
            }

            if (!Directory.Exists(Configuration.ThumbImagesPath))
            {
                Directory.CreateDirectory(Configuration.ThumbImagesPath);
            }

            foreach (var file in files)
            {
                var thumbPath = Path.Combine(Configuration.ThumbImagesPath, Path.GetFileName(file));

                if (File.Exists(thumbPath))
                {
                    File.Delete(thumbPath);
                }

                using (var image = Image.FromFile(file))
                using (var thumb = new Bitmap(Configuration.ThumbWidth, Configuration.ThumbHeight))
                using (var graphics = Graphics.FromImage(thumb))
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

                    graphics.DrawImage(image, new Rectangle(0, 0, Configuration.ThumbWidth, Configuration.ThumbHeight), crop, GraphicsUnit.Pixel);

                    SaveImage(thumb, thumbPath);
                }
            }
        }
    }
}
