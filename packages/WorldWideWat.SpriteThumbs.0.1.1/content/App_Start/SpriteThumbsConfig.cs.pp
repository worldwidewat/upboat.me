using System.IO;
using System.Web.Hosting;
using WorldWideWat.SpriteThumbs;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.SpriteThumbsConfig), "PostStart")]

namespace $rootnamespace$.App_Start 
{
    public static class SpriteThumbsConfig 
	{
        public static void PostStart() 
		{
            var configuration = SpriteThumbsGlobalConfiguration.Configuration;

            var outputFolder = HostingEnvironment.MapPath("/App_Data/SpriteThumbsOutput");

            if (outputFolder != null && !Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            configuration.SetSpriteOutputPath(outputFolder);
            
			// Add your images here
            // configuration.RawImages.Add(...);

			// Tweak other sprite generation parameters here (like thumb size and image quality)
			
            var spriteGenerator = new SpriteGenerator(SpriteThumbsGlobalConfiguration.Configuration);

            spriteGenerator.Generate();
        }
    }
}