using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpboatMe.App_Start;
using UpboatMe.Models;

namespace UpboatMeTests
{
    [TestClass]
    public class MemeConfigTests
    {
        [TestMethod]
        public void InitialismMultipleWordsTest()
        {
            // arrange
            var filenames = new string[]{"success-kid.png"};
            var expectedAlias = "sk";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedAlias, meme.Aliases[0]);
        }

        [TestMethod]
        public void InitialismMixedCaseTest()
        {
            // arrange
            var filenames = new string[] { "Success-Kid.png" };
            var expectedAlias = "sk";

            // act
            var memes = new MemeConfiguration();
            MemeConfig.AutoRegisterMemesByFile(memes, filenames);

            // assert
            Assert.AreEqual(1, memes.GetMemes().Count);
            var meme = memes.GetMemes()[0];

            Assert.AreEqual(expectedAlias, meme.Aliases[0]);
        }
    }
}
