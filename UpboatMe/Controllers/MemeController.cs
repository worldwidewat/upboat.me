using System.Web.Mvc;
using UpboatMe.App_Start;
using UpboatMe.Models;
using UpboatMe.Utilities;
using System.Linq;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        public ActionResult Make(string name, string top, string bottom, bool drawBoxes = false)
        {
            var sanitizedName = "";
            if (!string.IsNullOrEmpty(name))
            {
                sanitizedName = name.Replace("-", "");
            }

            var meme = GlobalMemeConfiguration.Memes[sanitizedName];
            
            if (meme == null)
            {
                // TODO: update this flow to return a proper HTTP 404 code, too
                meme = GlobalMemeConfiguration.NotFoundMeme;
                bottom = "Y U NO USE VALID MEME NAME?";
                top = "404";
            }

            var renderer = new Renderer();

            var bytes = renderer.Render(meme, top.SanitizeMemeText(), bottom.SanitizeMemeText(), drawBoxes);

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
    }
}
