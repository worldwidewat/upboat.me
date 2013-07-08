using System.IO;
using System.Web.Hosting;
using UpboatMe.Models;
using WorldWideWat.SpriteThumbs;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(UpboatMe.App_Start.SpriteThumbsConfig), "PostStart")]

namespace UpboatMe.App_Start 
{
    public static class SpriteThumbsConfig 
	{
        public static void PostStart() 
		{
            var configuration = SpriteThumbsGlobalConfiguration.Configuration;

            var outputFolder = HostingEnvironment.MapPath("~/App_Data/SpriteThumbsOutput");

            if (outputFolder != null && !Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            configuration.SetSpriteOutputPath(outputFolder);

            foreach (var meme in GlobalMemeConfiguration.Memes.GetMemes())
            {
                configuration.RawImages.Add(new RawImage
                {
                    Id = meme.ImageFileNameWithoutExtension,
                    FullFilePath = HostingEnvironment.MapPath(meme.ImagePath)

                });
            }
			
            var spriteGenerator = new SpriteGenerator(SpriteThumbsGlobalConfiguration.Configuration);

            spriteGenerator.Generate();
        }
    }
}