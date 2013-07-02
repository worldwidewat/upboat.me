using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpboatMe.App_Start;
using UpboatMe.Models;

namespace UpboatMeTests
{
    [TestClass]
    public class MemeConfigAliasTests
    {
        [TestMethod]
        public void InitialismMultipleWordsTest()
        {
            // arrange
            var filenames = new string[]{"01-success-kid.png"};
            const string expectedAlias = "sk";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }

        [TestMethod]
        public void InitialismOneWordTest()
        {
            // arrange
            var filenames = new string[] { "01-Fry.png" };
            const string expectedAlias = "fry"; // no single character aliases are allowed

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }
        
        [TestMethod]
        public void InitialismMixedCaseTest()
        {
            // arrange
            var filenames = new string[] { "01-Success-Kid.png" };
            const string expectedAlias = "sk";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }

        [TestMethod]
        public void InitialismWithApostropheTest()
        {
            // arrange
            var filenames = new string[] { "01-I'll-have-you-know.png" };
            const string expectedAlias = "ihyk";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }

        [TestMethod]
        public void InitialismWithSingleCharacterWordsTest()
        {
            // arrange
            var filenames = new string[] { "01-y-u-no.png" };
            const string expectedAlias = "yun";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }

        [TestMethod]
        public void InitialismWithNumberPrefix()
        {
            // arrange
            var filenames = new string[] { "01-1950s-guy.png" };
            const string expectedAlias = "1950g";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.IsTrue(meme.Aliases.Contains(expectedAlias));
        }
    }
}
