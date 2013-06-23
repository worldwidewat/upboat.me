using System;
using UpboatMe.Models;

namespace UpboatMe.Utilities
{
    public static class MemeUtilities
    {
        public static Meme FindMeme(MemeConfiguration memes, string memeName)
        {
            var sanitizedName = "";
            if (!String.IsNullOrEmpty(memeName))
            {
                sanitizedName = memeName.Replace("-", "");
            }

            var meme = memes[sanitizedName];

            return meme;
        }
    }
}