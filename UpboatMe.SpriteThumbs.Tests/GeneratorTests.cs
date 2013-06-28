using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UpboatMe.SpriteThumbs.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void TempGeneratorTest()
        {
            var configuration = new SpriteThumbsConfiguration();
            configuration.ImagePaths.Add(@"..\..\thumbs");
            configuration.SetOutputPath(@".\");
            configuration.SetImageQualityPercent(50);

            SpriteThumbsGenerator g = new SpriteThumbsGenerator(configuration);

            g.Generate();
        }
    }
}
