using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpboatMe.Utilities
{
    // via http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx
    public class ThreadRandom
    {
        private static readonly Random Global = new Random();

        [ThreadStatic]
        private static Random _local;

        public static int Next(int minValue, int maxValue)
        {
            var instance = _local;
            if (instance == null)
            {
                int seed;
                lock (Global) seed = Global.Next();
                _local = instance = new Random(seed);
            }
            return instance.Next(minValue, maxValue);
        }
    }
}