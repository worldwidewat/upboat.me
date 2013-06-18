using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpboatMe.Models;

namespace UpboatMe.App_Start
{
    public class GlobalMemeConfiguration
    {
        private const string NotFoundMemeName = "yuno";

        private static MemeConfiguration _memes;
        public static MemeConfiguration Memes { get { return _memes; } }

        public static Meme NotFoundMeme { get { return _memes[NotFoundMemeName]; } }

        static GlobalMemeConfiguration()
        {
            _memes = new MemeConfiguration();
        }
    }
}
