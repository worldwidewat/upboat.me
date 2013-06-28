using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpboatMe.Models;
using UpboatMe.Utilities;

namespace UpboatMeTests
{
    [TestClass]
    public class MemeUtilitiesTests
    {
        private static MemeConfiguration _MemeConfig;

        [TestInitialize]
        public void Init()
        {
            _MemeConfig = new MemeConfiguration();
            _MemeConfig.Add(new Meme("Success Kid", "", new[] {"sk", "successkid"}));
            _MemeConfig.Add(new Meme("I'll Have You Know", "", new[] {"ihyk", "illhaveyouknow"}));
            _MemeConfig.Add(new Meme("Foo", "", new[] {""}));
        }

        [TestMethod]
        public void FindMemeByNameWithDashes()
        {
            // arrange
            const string searchName = "success-kid";
            const string expectedName = "Success Kid";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeByNameWithoutDashes()
        {
            // arrange
            const string searchName = "successkid";
            const string expectedName = "Success Kid";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeNoMatchReturnsNull()
        {
            // arrange
            const string searchName = "foobar";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNull(actualMeme);
        }

        [TestMethod]
        public void FindMemeTrailingWords()
        {
            // arrange
            const string searchName = "youknow";
            const string expectedName = "I'll Have You Know";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeTrailingWordsWithDashes()
        {
            // arrange
            const string searchName = "you-know";
            const string expectedName = "I'll Have You Know";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeLeadingWords()
        {
            // arrange
            const string searchName = "illhave";
            const string expectedName = "I'll Have You Know";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeLeadingWordsWithDashes()
        {
            // arrange
            const string searchName = "ill-have";
            const string expectedName = "I'll Have You Know";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }
    }
}
