using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public class SpriteThumbsGenerator
    {
        private readonly SpriteThumbsConfiguration _configuration;

        public SpriteThumbsGenerator(SpriteThumbsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Generate()
        {
            foreach (var path in _configuration.ImagePaths)
            {
                var files = Directory.GetFiles(path, "*.jpg");

                if (!files.Any())
                {
                    continue;
                }

                var width = files.Length < _configuration.ThumbsPerRow ? files.Length : _configuration.ThumbsPerRow;
                var height = (int)Math.Ceiling(files.Length / (double)_configuration.ThumbsPerRow);
                
                if (File.Exists(_configuration.SpriteFilePath))
                {
                    File.Delete(_configuration.SpriteFilePath);
                }

                if (File.Exists(_configuration.StylesheetFilePath))
                {
                    File.Delete(_configuration.StylesheetFilePath);
                }

                using (var sprite = new Bitmap(width * _configuration.Width, height * _configuration.Height))
                using (var graphics = Graphics.FromImage(sprite))
                using (var stream = File.Open(_configuration.StylesheetFilePath, FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                {

                    for (int x = 0; x < files.Length; x++)
                    {
                        var columnIndex = x % _configuration.ThumbsPerRow;
                        var rowIndex = (int)Math.Floor(x / (double)_configuration.ThumbsPerRow);

                        var left = columnIndex * _configuration.Width;
                        var top = rowIndex * _configuration.Height;

                        WriteFileToSprite(files[x], graphics, left, top);
                        WriteFileCssInfo(Path.GetFileNameWithoutExtension(files[x]), writer, left, top);
                    }

                    var encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.FormatID == ImageFormat.Jpeg.Guid);

                    var qualityParameter = new EncoderParameter(Encoder.Quality, (long)_configuration.ImageQualityPercent);
                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = qualityParameter;


                    sprite.Save(_configuration.SpriteFilePath, encoder, encoderParameters);
                }
            }
        }

        private void WriteFileToSprite(string file, Graphics spriteGraphics, int left, int top)
        {
            using (var image = new Bitmap(file))
            {
                var bounds = new Rectangle(left, top, _configuration.Width, _configuration.Height);

                spriteGraphics.DrawImage(image, bounds);
            }
        }

        private void WriteFileCssInfo(string fileNameWithoutExtension, StreamWriter writer, int left, int top)
        {
            var entry = string.Format(".{0} {{ background-position: -{1}px -{2}px; width: {3}px; height: {4}px; background-image: url({5})}}",
                fileNameWithoutExtension, left, top, _configuration.Width, _configuration.Height, SpriteThumbsConfiguration.SpriteResource);

            writer.WriteLine(entry);
        }
    }
}
