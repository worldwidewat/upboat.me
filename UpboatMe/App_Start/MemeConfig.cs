using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UpboatMe.Models;

namespace UpboatMe.App_Start
{
    public static class MemeConfig
    {
        private static readonly Regex _StripCharactersToCollapseWords = new Regex(@"[']");
        private static readonly Regex _NonWordStripCharacters = new Regex(@"[_' -]", RegexOptions.Compiled);
        private static readonly Regex _ShouldBeDisplayedAsWhitespaceCharacters = new Regex(@"[_-]");

        public static void AutoRegisterMemesByFile(MemeConfiguration memes, string[] filenames)
        {
            foreach (var filename in filenames)
            {
                var lowerFilename = filename.ToLowerInvariant();
                var name = Path.GetFileNameWithoutExtension(lowerFilename);
                var extension = Path.GetExtension(lowerFilename).Substring(1); // strip off the dot

                var aliases = new List<string>
                                  {
                                      name.ToInitialism(),
                                      _NonWordStripCharacters.Replace(name, "")
                                  };

                var memeName = name.ToTitleString();

                var distinctAliases = aliases.Distinct().ToList();
                var imageType = "image/" + extension;
                 
                memes.Add(new Meme(memeName, filename, distinctAliases, imageType));
            }
        }

        public static string ToInitialism(this string input)
        {
            var collapsedWords = _StripCharactersToCollapseWords.Replace(input, "");
            var words = _NonWordStripCharacters.Split(collapsedWords);
            return string.Join("", words.Select(w => w[0]));
        }

        public static string ToTitleString(this string name)
        {
            // first drop names from something like "I'll-have-you-know" to "Ill-have-you-know"
            // then fix the separators so it becomes "I'll have you know"
            var memeName = _ShouldBeDisplayedAsWhitespaceCharacters.Replace(name, " ");

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(memeName);
        }

        public static void RegisterManualMemes(MemeConfiguration memes)
        {
            // overrides
            memes["10guy"].Aliases.Add("tenguy");
            memes["angrywalter"].Aliases.Add("amitheonlyone");
            memes["angrywalter"].Aliases.Add("aitoo");
            memes["annoyedpicard"].Aliases.Add("whythefuck");
            memes["braceyourselves"].Aliases.Add("imminentned");
            memes["braceyourselves"].Aliases.Add("iscoming");
            memes["ermahgerd"].Aliases.Add("emg");
            memes["everyonelosestheirminds"].Aliases.Add("joker");
            memes["futuramafry"].Aliases.Add("notsureif");
            memes["grumpycat"].Aliases.Add("sadcat");
            memes["idontwanttoliveonthisplanetanymore"].Aliases.Add("farnsworth");
            memes["illhaveyouknow"].Aliases.Add("spongebob");
            memes["officespacelumbergh"].Aliases.Add("thatdbegreat");
            memes["officespacelumbergh"].Aliases.Add("yeah");
            memes["onedoesnotsimply"].Aliases.Add("boromir");
            memes["onedoesnotsimply"].Aliases.Add("mordor");
            memes["schrutefacts"].Aliases.Add("schrute");
            memes["schrutefacts"].Aliases.Add("dwight");
            memes["schrutefacts"].Aliases.Add("correction");
            memes["schrutefacts"].Aliases.Add("false");
            memes["technologicallyimpairedduck"].Aliases.Add("technologyduck");
            memes["boythatescalatedquickly"].Aliases.Add("wteq");
            memes["boythatescalatedquickly"].Aliases.Add("ronburgundy");
            memes["themostinterestingmanintheworld"].Aliases.Add("idontalways");
            memes["toystoryeverywhere"].Aliases.Add("buzzwoody");
            memes["unhelpfulhighschoolteacher"].Aliases.Add("scumbagteacher");
        }
    }
}
