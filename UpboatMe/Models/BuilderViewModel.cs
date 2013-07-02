using System.Collections.Generic;
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

        public string GetPreviewUrl(UrlHelper helper)
        {
            return helper.AbsoluteAction(helper.Action("Make", "Meme", new { name = SelectedMeme, Top, Bottom }));
        }

        public BuilderViewModel()
        {
            SelectedMeme = "ihyk";
            Top = "I'll-have-you-know-I-tried-other-meme-generators";
            Bottom = "and-only-wasted-hours-and-hours-of-my-life";
        }
    }
}