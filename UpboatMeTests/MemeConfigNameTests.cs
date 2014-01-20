using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpboatMe.App_Start;
using UpboatMe.Models;

namespace UpboatMeTests
{
    [TestClass]
    public class MemeConfigNameTests
    {
        [TestMethod]
        public void DashesReplacedWithSpacesTest()
        {
            // arrange
            var filenames = new string[] { "100-Success-Kid.png" };
            const string expectedDescription = "Success Kid";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedDescription, meme.Description);
        }

        [TestMethod]
        public void TitleCaseTest()
        {
            // arrange
            var filenames = new string[] { "100-success-kid.png" };
            const string expectedDescription = "Success Kid";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedDescription, meme.Description);
        }

        [TestMethod]
        public void NameWithApostropheTest()
        {
            // arrange
            var filenames = new string[] { "100-I'll-have-you-know.png" };
            const string expectedDescription = "I'll Have You Know";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedDescription, meme.Description);
        }

        [TestMethod]
        public void NameWithJpgExtensionTest()
        {
            // arrange
            var filenames = new string[] { "100-file.jpg" };
            const string expectedDescription = "File";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedDescription, meme.Description);
        }

        [TestMethod]
        public void NameWithTwoCharacterExtensionTest()
        {
            // arrange
            var filenames = new string[] { "100-file.jp" };
            const string expectedDescription = "File";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedDescription, meme.Description);
        }
    }
}
