using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        private readonly Regex _previewUrlStripper = new Regex(@"\s+", RegexOptions.Compiled);
        public MvcHtmlString GetPreviewUrl(UrlHelper helper)
        {
            var root = HttpContext.Current.Request.ApplicationPath ?? "/";
            if (!root.EndsWith("/"))
            {
                root += "/";
            }

            var path = string.Format("{0}{1}/{2}/{3}", root, SelectedMeme, Top, Bottom);
            var strippedPath = _previewUrlStripper.Replace(path, "-");
            var trimmedPath = strippedPath.TrimEnd('/');
            var pathWithExtension = trimmedPath + ".jpg";

            return MvcHtmlString.Create(helper.AbsoluteAction(pathWithExtension));
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