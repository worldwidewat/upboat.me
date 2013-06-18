using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UpboatMe.App_Start;
using UpboatMe.Models;
using UpboatMe.Utilities;

namespace UpboatMe.Controllers
{
    public class MemeController : Controller
    {
        public ActionResult Make(string name, string top, string bottom)
        {
            var meme = GlobalMemeConfiguration.Memes[name];
            
            if (meme == null)
            {
                meme = GlobalMemeConfiguration.NotFoundMeme;
                bottom = "Y U NO USE VALID MEME NAME?";
                top = "404";
            }

            var renderer = new Renderer(meme, top, bottom);

            return new FileContentResult(renderer.Render(), meme.ImageType);   
        }
    }
}
