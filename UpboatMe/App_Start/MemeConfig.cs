﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UpboatMe.Models;
using UpboatMe.Utilities;

namespace UpboatMe.App_Start
{
    public static class MemeConfig
    {
        private static readonly Regex StripCharactersToCollapseWords = new Regex(@"[']");
        private static readonly Regex NonWordStripCharacters = new Regex(@"[_' -]", RegexOptions.Compiled);
        private static readonly Regex ShouldBeDisplayedAsWhitespaceCharacters = new Regex(@"[_-]");

        public static void AutoRegisterMemesByFile(MemeConfiguration memes, string[] filenames)
        {
            var usedAliases = new HashSet<string>();

            foreach (var filename in filenames.OrderBy(f => f))
            {
                var lowerFilename = filename.ToLowerInvariant();

                var name = Path.GetFileNameWithoutExtension(lowerFilename);

                if (name == null)
                {
                    throw new NullReferenceException("name cannot be null");
                }
                // strip off number prefix (used for ordering)
                name = name.Substring(lowerFilename.IndexOf("-", StringComparison.Ordinal) + 1);

                var extension = Path.GetExtension(lowerFilename);
                
                if (extension == null)
                {
                    throw new NullReferenceException("extension cannot be null");
                }
                extension = extension.Substring(1); // strip off the dot

                var aliases = new List<string>
                                  {
                                      name.ToInitialism(),
                                      NonWordStripCharacters.Replace(name, ""),
                                  };

                var memeName = name.ToTitleString();

                var filteredAliases =
                    aliases.Where(a => a.Length > 1) // don't use single character aliases
                           .Distinct()
                           .ToList();

                var survivingAliases = new List<string>();
                // Note: don't follow resharper's advice on this loop. It be cray cray
                foreach (var alias in filteredAliases)
                {
                    if (usedAliases.Add(alias))
                    {
                        survivingAliases.Add(alias);
                    }
                }

                // TODO: image/jpg isn't acutally valid. Fix this or get rid of it
                var imageType = "image/" + extension;

                memes.Add(new Meme(memeName, filename, survivingAliases, imageType));
            }
        }

        private static readonly Regex DigitPrefixRegex = new Regex(@"^(\d+)", RegexOptions.Compiled);
        public static string ToInitialism(this string input)
        {
            var collapsedWords = StripCharactersToCollapseWords.Replace(input, "");
            var words = NonWordStripCharacters.Split(collapsedWords);
            var wordParts = words.Select(w =>
                                             {
                                                 // if the prefix is a number, take the whole number
                                                 var possibleNumberPrefix = DigitPrefixRegex.Match(w);
                                                 if (possibleNumberPrefix.Captures.Count > 0)
                                                 {
                                                     return possibleNumberPrefix.Captures[0].Value;
                                                 }
                                                 // otherwise just take the first character
                                                 return w.Substring(0, 1);
                                             });

            return string.Join("", wordParts);
        }

        public static string ToTitleString(this string name)
        {
            // first drop names from something like "I'll-have-you-know" to "Ill-have-you-know"
            // then fix the separators so it becomes "I'll have you know"
            var memeName = ShouldBeDisplayedAsWhitespaceCharacters.Replace(name, " ");

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(memeName);
        }

        public static readonly PrivateFontCollection PrivateFontCollection = new PrivateFontCollection();

        public static void RegisterManualMemes(MemeConfiguration memes)
        {
            var batmanFontPath = HttpRuntime.AppDomainAppPath + @"Fonts\SFActionManExtended.ttf";
            PrivateFontCollection.AddFontFile(batmanFontPath);

            // overrides
            memes["10guy"].Aliases.Add("tenguy");
            memes["allthethings"].Aliases.Add("xallthey");
            memes["angrywalter"].Aliases.Add("amitheonlyone", "aitoo");
            memes["annoyedpicard"].Aliases.Add("whythefuck");
            memes["braceyourselves"].Aliases.Add("imminentned", "iscoming");
            memes["ermahgerd"].Aliases.Add("emg");
            memes["everyonelosestheirminds"].Aliases.Add("joker");
            memes["futuramafry"].Aliases.Add("notsureif");
            memes["grumpycat"].Aliases.Add("sadcat");
            memes["idontwanttoliveonthisplanetanymore"].Aliases.Add("farnsworth");
            memes["illhaveyouknow"].Aliases.Add("spongebob");
            memes["officespacelumbergh"].Aliases.Add("thatdbegreat", "yeah");
            memes["onedoesnotsimply"].Aliases.Add("boromir", "mordor");
            memes["schrutefacts"].Aliases.Add("schrute", "dwight", "correction", "false");
            memes["technologicallyimpairedduck"].Aliases.Add("technologyduck");
            memes["boythatescalatedquickly"].Aliases.Add("wteq", "ronburgundy");
            memes["themostinterestingmanintheworld"].Aliases.Add("idontalways");
            memes["toystoryeverywhere"].Aliases.Add("buzzwoody", "xxeverywhere");
            memes["unhelpfulhighschoolteacher"].Aliases.Add("scumbagteacher");
            memes["mckaylamaroneynotimpressed"].Aliases.Add("nim", "um", "unimpressedmckayla");

            var batman = memes["batmanslappingrobin"];
            batman.Font = "SF Action Man Extended";
            batman.Fill = Color.FromArgb(255, 63, 63, 63);
            batman.StrokeWidth = -1;
            batman.FontStyle = FontStyle.Italic;
            batman.TopLineBounds = new Rectangle(10, 5, 180, 75);
            batman.BottomLineBounds = new Rectangle(220, 5, 170, 75);

            var resharper = memes["resharpertip"];
            resharper.Font = "Segoe UI";
            resharper.TextAlignment = StringAlignment.Near;
            resharper.Fill = Color.WhiteSmoke;
            resharper.StrokeWidth = -1;
            resharper.TopLineBounds = new Rectangle(78, 72, 481, 22);
            resharper.BottomLineBounds = new Rectangle(78, 97, 462, 22);
            resharper.DoForceTextToAllCaps = false;

            var doge = memes["doge"];
            doge.Font = "Comic Sans MS";
        }
    }
}
