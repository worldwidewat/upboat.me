using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UpboatMe.Utilities;

namespace UpboatMe.Models
{
    public class BuilderViewModel
    {
        public string SelectedMeme { get; set; }
        public string Top { get; set; }
        public string Bottom { get; set; }
        public IList<Meme> Memes { get; set; }

        public MvcHtmlString GetPreviewUrl(UrlHelper helper)
        {
            var root = HttpContext.Current.Request.ApplicationPath ?? "/";
            if (!root.EndsWith("/"))
            {
                root += "/";
            }

            var path = string.Format("{0}{1}/{2}/{3}.jpg", root, SelectedMeme, Top, Bottom);

            return MvcHtmlString.Create(helper.AbsoluteAction(path));
        }

        public string GetAltText()
        {
            return string.Format("{0}:{1}{2}{1}{3}",
                                 SelectedMeme, Environment.NewLine, Top, Bottom);
        }

        public string GetShareText()
        {
            return string.Format("{0}", SelectedMeme);
        }
    }
}