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
            _MemeConfig.Add(new Meme("Foo", "", new[] { "f", "foo" }));
            _MemeConfig.Add(new Meme("All The Things", "", new[] { "att", "allthethings" }));
            _MemeConfig.Add(new Meme("Chubby Bubbles Girl", "", new[] {"cbg", "chubbybubblesgirl"}));
            _MemeConfig.Add(new Meme("Confession Bear", "", new[] {"cb", "confessionbear"}));
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
            const string searchName = "blah";

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

        [TestMethod]
        public void FindMemeSearchNameLeadsWithMemeName()
        {
            // arrange
            const string searchName = "ill-have-you-know-i-watched-the-notebook-and-only-cried-four-times";
            const string expectedName = "I'll Have You Know";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindMemeSearchNameTrailsWithMemeName()
        {
            // arrange
            const string searchName = "search-for-all-the-things";
            const string expectedName = "All The Things";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindConfessionBearByInitialism()
        {
            // arrange
            const string searchName = "cb";
            const string expectedName = "Confession Bear";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }

        [TestMethod]
        public void FindConfessionBearByName()
        {
            // arrange
            const string searchName = "confession-bear";
            const string expectedName = "Confession Bear";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }
    }
}
