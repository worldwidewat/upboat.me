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

            configuration.SetSpriteOutputPath(outputFolder);

            configuration.SetThumbImagesPath(HostingEnvironment.MapPath("/App_Data/GeneratedThumbs"));

            configuration.SetRawImagesPath(HostingEnvironment.MapPath("/images"));
        }
    }
}