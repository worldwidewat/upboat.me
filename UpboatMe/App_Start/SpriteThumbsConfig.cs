using System.IO;
using System.Web.Hosting;
using UpboatMe.SpriteThumbs;

namespace UpboatMe.App_Start
{
    public static class SpriteThumbsConfig
    {
        public static void Initialize(SpriteThumbsConfiguration configuration)
        {
            var outputFolder = HostingEnvironment.MapPath("/App_Data/GeneratedThumbs");

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            configuration.SetOutputPath(outputFolder);

            configuration.ImagePaths.Add(HostingEnvironment.MapPath("/images/thumbs"));
        }
    }
}