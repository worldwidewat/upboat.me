using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public abstract class GeneratorBase
    {
        protected SpriteThumbsConfiguration Configuration { get; private set; }

        protected GeneratorBase(SpriteThumbsConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SaveImage(Image image, string path)
        {
            var encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.FormatID == ImageFormat.Jpeg.Guid);

            var qualityParameter = new EncoderParameter(Encoder.Quality, (long)Configuration.ImageQualityPercent);
            var encoderParameters = new EncoderParameters(1);

            encoderParameters.Param[0] = qualityParameter;

            image.Save(path, encoder, encoderParameters);
        }
    }
}
