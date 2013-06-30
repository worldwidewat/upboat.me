using System;
using System.IO;
using ApprovalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpboatMe.Imaging;
using UpboatMe.Models;

namespace UpboatMeTests
{
    [TestClass]
    public class ImageIntegrationTests
    {
        [TestMethod]
        public void TextRendererTest()
        {
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "y-u-no.jpg");
            var meme = new Meme("test", filepath, null);

            var renderParameters = new RenderParameters
                                       {
                                           FullImagePath = meme.ImageFileName,
                                           TopLineHeightPercent = meme.TopLineHeightPercent,
                                           BottomLineHeightPercent = meme.BottomLineHeightPercent,
                                           Fill = meme.Fill,
                                           Stroke = meme.Stroke,
                                           Font = meme.Font,
                                           FontSize = meme.FontSize,
                                           StrokeWidth = meme.StrokeWidth,
                                           DrawBoxes = false
                                       };

            var renderer = new Renderer();

            var bytes = renderer.Render(renderParameters, "foo", "bar");

            Approvals.Verify("test");
        }

        [TestMethod]
        public void test()
        {
            Approvals.Verify("foo");
        }
    }
}
