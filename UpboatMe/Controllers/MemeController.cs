using System.Text.RegularExpressions;
using System.Web.Mvc;
using UpboatMe.App_Start;
using UpboatMe.Models;
using UpboatMe.Utilities;
using System.Linq;
using GoogleAnalyticsTracker;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        private static Regex _StripRegex = new Regex(@"&?drawboxes=true|\.png$|\.jpe?g$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        // ignore the parameters and figure it out manually from Request.RawUrl
        // this allows us to keep using routes to generate our own links, which is handy
        public ActionResult Make(string name, string top, string bottom)
        {
            // Request.RawUrl doesn't handle double slashes correctly, e.g. /sk//foo,
            // but this server variable does, apparently
            var url = Request.ServerVariables["UNENCODED_URL"]; // Request.RawUrl;
            
            // todo - handle this more elegantly, or don't do such things via this action
            var drawBoxes = url.Contains("drawBoxes=true");
            
            // strip off any file extensions
            url = _StripRegex.Replace(url, "");
            
            var memeRequest = MemeUtilities.GetMemeRequest(url);

            var meme = MemeUtilities.FindMeme(GlobalMemeConfiguration.Memes, memeRequest.Name);
            if (meme == null)
            {
                // TODO: update this flow to return a proper HTTP 404 code, too
                meme = GlobalMemeConfiguration.NotFoundMeme;
                memeRequest.Top = "404";
                memeRequest.Bottom = "Y U NO USE VALID MEME NAME?";
            }

            var renderer = new Renderer();

            var bytes = renderer.Render(meme, memeRequest.Top.SanitizeMemeText(), memeRequest.Bottom.SanitizeMemeText(), drawBoxes);

            Analytics.TrackMeme(HttpContext, memeRequest.Name);

            return new FileContentResult(bytes, meme.ImageType);
        }

        public ActionResult Debug(string top, string bottom)
        {
            var viewModel = new MemeDebugViewModel();

            viewModel.DebugImages = GlobalMemeConfiguration.Memes.GetMemeNames().Select(m => Url.Action("Make",
                new
                {
                    name = m,
                    top = top,
                    bottom = bottom,
                    drawBoxes = Request.QueryString["drawBoxes"]
                }))
                .ToList();

            return View(viewModel);
        }

        public ActionResult List()
        {
            var list = GlobalMemeConfiguration.Memes.GetMemes();

            return View(list);
        }
    }
}
