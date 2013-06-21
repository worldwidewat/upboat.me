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
            var meme = GlobalMemeConfiguration.Memes[name];
            
            if (meme == null)
            {
                meme = GlobalMemeConfiguration.NotFoundMeme;
                bottom = "Y U NO USE VALID MEME NAME?";
                top = "404";
            }

            var renderer = new Renderer();

            var bytes = renderer.Render(meme, top.SanitizeMemeText(), bottom.SanitizeMemeText(), drawBoxes);

            return new FileContentResult(bytes, meme.ImageType);   
        }

        public ActionResult Debug()
        {
            var viewModel = new MemeDebugViewModel();

            viewModel.DebugImages = GlobalMemeConfiguration.Memes.Select(m => Url.Action("Make", new { name = m, drawBoxes = true })).ToList();

            return View(viewModel);
        }
    }
}
