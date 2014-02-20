using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI;
using UpboatMe.App_Start;
using UpboatMe.Imaging;
using UpboatMe.Models;
using UpboatMe.Utilities;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        private static readonly Regex UrlExtension = new Regex(@"\.png$|\.jpe?g$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [OutputCache(Location = OutputCacheLocation.Any, Duration = 60 * 60 /* 1 hour */)]
        public ActionResult Make()
        {
            var url = Request.ServerVariables["HTTP_URL"];
            var memeRequest = MemeRequest.FromUrl(url, Server);
            var meme = MemeUtilities.FindMeme(GlobalMemeConfiguration.Memes, memeRequest.Name);
            if (meme == null)
            {
                // TODO: update this flow to return a proper HTTP 404 code, too
                meme = GlobalMemeConfiguration.NotFoundMeme;
                memeRequest.Lines = new List<string> { "404", "Y U NO USE VALID MEME NAME?" };
            }

            // if we want to do this earlier in the process consider
            // if we still need to worry about png vs. jpg. The browser
            // probably doesn't care, but it might be weird if the extension
            // doesn't match the mimetype
            var hasExtension = UrlExtension.IsMatch(url);
            if (!hasExtension)
            {
                return Redirect(url + Path.GetExtension(meme.ImageFileName));
            }

            var renderParameters = new RenderParameters
            {
                FullImagePath = HttpContext.Server.MapPath(meme.ImagePath),
                DebugMode = memeRequest.IsDebugMode,
                FullWatermarkImageFilePath = HttpContext.Server.MapPath("~/Content/UpBoatWatermark.png"),
                WatermarkImageHeight = 25,
                WatermarkImageWidth = 25,
                WatermarkText = "upboat.me",
                WatermarkFont = "Arial",
                WatermarkFontStyle = FontStyle.Regular,
                WatermarkFontSize = 9,
                WatermarkStroke = Color.Black,
                WatermarkFill = Color.White,
                WatermarkStrokeWidth = 1,
                PrivateFonts = MemeConfig.PrivateFontCollection,
                Lines = meme.Lines.Select(l => new LineParameters
                {
                    Bounds = l.Bounds,
                    DoForceTextToAllCaps = l.DoForceTextToAllCaps,
                    Fill = l.Fill,
                    Font = l.Font,
                    FontSize = l.FontSize,
                    FontStyle = l.FontStyle,
                    HeightPercent = l.HeightPercent,
                    Stroke = l.Stroke,
                    StrokeWidth = l.StrokeWidth,
                    TextAlignment = l.TextAlignment,
                    HugBottom = l.HugBottom
                }).ToList()
            };

            for(var x = 0; x < renderParameters.Lines.Count; x++)
            {
                if (x < memeRequest.Lines.Count)
                {
                    renderParameters.Lines[x].Text = memeRequest.Lines[x].SanitizeMemeText(renderParameters.Lines[x].DoForceTextToAllCaps);
                }
            }

            var renderer = new Renderer();

            var bytes = renderer.Render(renderParameters);

            Analytics.TrackMeme(HttpContext, memeRequest.Name);

            return new FileContentResult(bytes, meme.ImageType);
        }

        public ActionResult Debug(string top, string bottom)
        {
            var viewModel = new MemeDebugViewModel();

            viewModel.DebugImages = GlobalMemeConfiguration.Memes.GetMemeNames().Select(m => string.Format("/{0}/{1}/{2}", m, top, bottom)).ToList();

            return View(viewModel);
        }

        [OutputCache(Location = OutputCacheLocation.Any, Duration = 60)]
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

        [OutputCache(Location = OutputCacheLocation.Any, Duration = 60)]
        public ActionResult Builder()
        {
            var url = Request.ServerVariables["HTTP_URL"];
            var memeRequest = MemeRequest.FromUrl(url, Server);

            var meme = MemeUtilities.FindMeme(GlobalMemeConfiguration.Memes, memeRequest.Name);
            if (meme == null)
            {
                memeRequest.Name = "ihyk";
                memeRequest.Lines = new List<string>() { "I'll-have-you-know-I-tried-other-meme-generators", "and-only-wasted-hours-and-hours-of-my-life" };
            }

            var viewModel = new BuilderViewModel
            {
                Memes = GlobalMemeConfiguration.Memes.GetMemes(),
                SelectedMeme = meme != null ? meme.Aliases.First() : memeRequest.Name,
                Lines = meme.Lines.Select((item, index) => memeRequest.Lines.Count > index ? memeRequest.Lines[index] : string.Empty).ToList()
            };

            return View(viewModel);
        }
    }
}