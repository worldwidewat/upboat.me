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
        public IList<Meme> Memes { get; set; }
        public List<string> Lines { get; set; }

        private readonly Regex _previewUrlStripper = new Regex(@"\s+", RegexOptions.Compiled);
        public MvcHtmlString GetPreviewUrl(UrlHelper helper)
        {
            var root = HttpContext.Current.Request.ApplicationPath ?? "/";
            if (!root.EndsWith("/"))
            {
                root += "/";
            }

            var path = string.Format("{0}{1}/{2}", root, SelectedMeme, string.Join("/", Lines));
            var strippedPath = _previewUrlStripper.Replace(path, "-");
            var trimmedPath = strippedPath.TrimEnd('/');
            var pathWithExtension = trimmedPath + ".jpg";

            return MvcHtmlString.Create(helper.AbsoluteAction(pathWithExtension));
        }

        public string GetAltText()
        {
            return string.Format("{0}:{1}{2}",
                                 SelectedMeme, Environment.NewLine, string.Join(Environment.NewLine, Lines));
        }

        public string GetShareText()
        {
            return string.Format("{0}", SelectedMeme);
        }
    }
}