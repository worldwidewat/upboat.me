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
            _MemeConfig.Add(new Meme("success-kid", "", new[] { "sk", "successkid" }));
            _MemeConfig.Add(new Meme("foo", "", new[] { "" }));

        }
        [TestMethod]
        public void FindMemeByNameWithDashesWorks()
        {
            // arrange
            const string searchName = "success-kid";
            const string expectedName = "success-kid";

            // act
            var actualMeme = MemeUtilities.FindMeme(_MemeConfig, searchName);

            // assert
            Assert.IsNotNull(actualMeme);
            Assert.AreEqual(expectedName, actualMeme.Description);
        }
    }
}
