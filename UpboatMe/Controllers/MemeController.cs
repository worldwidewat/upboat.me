using System.Web.Mvc;
using UpboatMe.App_Start;
using UpboatMe.Utilities;

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
    }
}
