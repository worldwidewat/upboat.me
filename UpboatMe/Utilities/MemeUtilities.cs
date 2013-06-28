using System;
using System.Linq;
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

            var meme = memes[sanitizedName]
                       ?? memes.GetMemes().FirstOrDefault(m => m.Aliases.Any(a => a.StartsWith(sanitizedName)))
                       ?? memes.GetMemes().FirstOrDefault(m => m.Aliases.Any(a => a.EndsWith(sanitizedName)));

            return meme;
        }

        public static MemeRequest GetMemeRequest(string url)
        {
            var result = new MemeRequest();

            if (String.IsNullOrEmpty(url))
            {
                return result;
            }

            // urls look like this:  /meme-name/first-line/second-line
            // so we're basically delimiting on slashes
            // if there are surprise slashes, they'll end up in the second line

            // strip off that first slash--it isn't helpful
            url = url.Substring(1);

            var firstSlash = url.IndexOf('/');
            var secondSlash = url.IndexOf('/', firstSlash + 1);

            if (firstSlash > 0)
            {
                result.Name = url.Substring(0, firstSlash);
            }
            else
            {
                // we only have the meme name (and maybe not even that!). Time to bail out
                result.Name = url;
                return result;
            }

            if (secondSlash > 0)
            {
                result.Top = url.Substring(firstSlash + 1, secondSlash - firstSlash - 1);
                result.Bottom = url.Substring(secondSlash + 1);
            }
            else
            {
                result.Top = url.Substring(firstSlash + 1);
            }

            return result;

        }
    }
}