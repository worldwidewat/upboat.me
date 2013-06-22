using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpboatMe.Models;

namespace UpboatMe.App_Start
{
    public static class MemeConfig
    {
        public static void RegisterMemes(MemeConfiguration memes)
        {
            memes.Add(new Meme("10-guy"), "10guy", "tenguy");
            memes.Add(new Meme("actual-advice-mallard"), "aam", "actualadvicemallard");
            memes.Add(new Meme("all-the-things"), "att", "allthethings");
            memes.Add(new Meme("bad-joke-eel"), "bje", "badjokeeel");
            memes.Add(new Meme("bad-luck-brian"), "blb", "badluckbrian");
            memes.Add(new Meme("everyone-loses-their-minds"), "eltm", "everyonelosestheirminds");
            memes.Add(new Meme("first-world-problems"), "fwp", "firstworldproblems");
            memes.Add(new Meme("futurama-fry"), "ff", "futuramafry", "fry");
            memes.Add(new Meme("good-guy-greg"), "ggg", "goodguygreg");
            memes.Add(new Meme("ill-have-you-know"), "ihyk", "illhaveyouknow");
            memes.Add(new Meme("malicious-advice-mallard"), "mam", "maliciousadvicemallard");
            memes.Add(new Meme("overly-attached-girlfriend"), "oag", "overlyattachedgirlfriend");
            memes.Add(new Meme("overly-manly-man"), "omm", "overlymanlyman");
            memes.Add(new Meme("scumbag-steve"), "ss", "scumbagsteve");
            memes.Add(new Meme("socially-awesome-awkward-penguin"), "sociallyaweomeawkwardpenguin", "awesomeawkwardpenguin");
            memes.Add(new Meme("socially-awkward-awesome-penguin"), "sociallyawkwardawesomepenguin", "awkwardawesomepenguin");
            memes.Add(new Meme("socially-awkward-penguin"), "sap", "sociallyawkwardpenguin");
            memes.Add(new Meme("success-kid"), "sk", "successkid");
            memes.Add(new Meme("sudden-clarity-clarence"), "scc", "suddenclarityclarence");
            memes.Add(new Meme("the-most-interesting-man-in-the-world"), "mim", "tmim", "tmimitw", "themostinterestingmanintheworld", "mostinterestingman");
            memes.Add(new Meme("y-u-no", bottomLineHeight: 80), "yuno");
        }
    }
}