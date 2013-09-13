using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using UpboatMe.App_Start;
using UpboatMe.Imaging;
using UpboatMe.Models;
using UpboatMe.Utilities;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        private static Regex _UrlExtension = new Regex(@"\.png$|\.jpe?g$", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        // ignore the parameters and figure it out manually from Request.RawUrl
        // this allows us to keep using routes to generate our own links, which is handy
        public ActionResult Make()
        {
            var url = Request.ServerVariables["HTTP_URL"];
            var memeRequest = MemeRequest.FromUrl(url, Server);
            var meme = MemeUtilities.FindMeme(GlobalMemeConfiguration.Memes, memeRequest.Name);
            if (meme == null)
            {
                // TODO: update this flow to return a proper HTTP 404 code, too
                meme = GlobalMemeConfiguration.NotFoundMeme;
                memeRequest.Top = "404";
                memeRequest.Bottom = "Y U NO USE VALID MEME NAME?";
            }

            // if we want to do this earlier in the process consider
            // if we still need to worry about png vs. jpg. The browser
            // probably doesn't care, but it might be weird if the extension
            // doesn't match the mimetype
            var hasExtension = _UrlExtension.IsMatch(url);
            if (!hasExtension)
            {
                return Redirect(url + Path.GetExtension(meme.ImageFileName));
            }

            var renderParameters = new RenderParameters
            {
                FullImagePath = HttpContext.Server.MapPath(meme.ImagePath),
                TopLineHeightPercent = meme.TopLineHeightPercent,
                TopLineBounds = meme.TopLineBounds,
                BottomLineHeightPercent = meme.BottomLineHeightPercent,
                BottomLineBounds = meme.BottomLineBounds,
                Fill = meme.Fill,
                Stroke = meme.Stroke,
                Font = meme.Font,
                FontSize = meme.FontSize,
                StrokeWidth = meme.StrokeWidth,
                DebugMode = memeRequest.IsDebugMode,
                FullWatermarkImageFilePath = HttpContext.Server.MapPath("~/Content/UpBoatWatermark.png"),
                WatermarkImageHeight = 25,
                WatermarkImageWidth = 25,
                WatermarkText = "upboat.me",
                WatermarkFont = "Arial",
                WatermarkFontSize = 9,
                WatermarkStroke = Color.Black,
                WatermarkFill = Color.White,
                WatermarkStrokeWidth = 1,
                PrivateFonts = MemeConfig.PrivateFontCollection,
                FontStyle = meme.FontStyle
            };

            var renderer = new Renderer();

            var bytes = renderer.Render(renderParameters, memeRequest.Top.SanitizeMemeText(), memeRequest.Bottom.SanitizeMemeText());

            Analytics.TrackMeme(HttpContext, memeRequest.Name);

            return new FileContentResult(bytes, meme.ImageType);
        }

        public ActionResult Debug(string top, string bottom)
        {
            var viewModel = new MemeDebugViewModel();

            viewModel.DebugImages = GlobalMemeConfiguration.Memes.GetMemeNames().Select(m => string.Format("/{0}/{1}/{2}", m, top, bottom)).ToList();

            return View(viewModel);
        }

        public ActionResult List(string query)
        {
            var list = GlobalMemeConfiguration.Memes.GetMemes();
            if (string.IsNullOrEmpty(query))
            {
                return View(list);
            }

            var filteredList = list.AsQueryable();
            var keywords = query.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var keyword in keywords)
            {
                var k = keyword.Trim();

                if (k.StartsWith("-"))
                {
                    k = k.TrimStart('-');
                    if (k == "")
                    {
                        continue;
                    }

                    filteredList = filteredList
                        .Where(m => m.Description.IndexOf(k, StringComparison.OrdinalIgnoreCase) == -1
                                    && m.Aliases.All(a => a.IndexOf(k, StringComparison.OrdinalIgnoreCase) == -1));
                }
                else
                {
                    filteredList = filteredList
                        .Where(m => m.Description.IndexOf(k, StringComparison.OrdinalIgnoreCase) != -1
                                    || m.Aliases.Any(a => a.IndexOf(k, StringComparison.OrdinalIgnoreCase) != -1));
                }
            }

            return View(filteredList.ToList());
        }

        public ActionResult Builder()
        {
            var url = Request.ServerVariables["HTTP_URL"];
            var memeRequest = MemeRequest.FromUrl(url, Server);

            var meme = MemeUtilities.FindMeme(GlobalMemeConfiguration.Memes, memeRequest.Name);
            if (meme == null)
            {
                // TODO: update this flow to return a proper HTTP 404 code, too
                memeRequest.Name = "ihyk";
                memeRequest.Top = "I'll-have-you-know-I-tried-other-meme-generators";
                memeRequest.Bottom = "and-only-wasted-hours-and-hours-of-my-life";
            }

            var viewModel = new BuilderViewModel
            {
                Memes = GlobalMemeConfiguration.Memes.GetMemes(),
                SelectedMeme = meme != null ? meme.Aliases.First() : memeRequest.Name,
                Top = memeRequest.Top,
                Bottom = memeRequest.Bottom
            };

            return View(viewModel);
        }
    }
}
