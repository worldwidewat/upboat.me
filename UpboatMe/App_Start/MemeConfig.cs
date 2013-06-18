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
            memes.Add(new Meme("success-kid"), "sk", "successkid");
        }
    }
}