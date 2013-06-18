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
        private static MemeConfiguration _memes;
        public static MemeConfiguration Memes { get { return _memes; } }

        static GlobalMemeConfiguration()
        {
            _memes = new MemeConfiguration();
        }   
    }
}
