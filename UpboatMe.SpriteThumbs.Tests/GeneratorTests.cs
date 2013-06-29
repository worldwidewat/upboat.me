using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UpboatMe.SpriteThumbs.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void TempSpriteGeneratorTest()
        {
            var configuration = new SpriteThumbsConfiguration();
            configuration.SetThumbImagesPath(@"..\..\thumbs");
            configuration.SetSpriteOutputPath(@".\");
            configuration.SetImageQualityPercent(50);

            SpriteGenerator g = new SpriteGenerator(configuration);

            g.Generate();
        }

        [TestMethod]
        public void TempThumbsGeneratorTest()
        {
            var configuration = new SpriteThumbsConfiguration();

            configuration.SetRawImagesPath(@".\");
            configuration.SetThumbImagesPath(@".\thumbs");

            var generator = new ThumbsGenerator(configuration);

            generator.Generate();
        }
    }
}
