using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UpboatMe.Utilities
{
    public static class SloganUtilities
    {
        private static string[] _Slogans { get; set; }

        private static string[] Slogans
        {
            get
            {
                if (_Slogans == null)
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/App_Data/slogans.txt");
                    _Slogans = File.ReadAllLines(filePath);
                }

                return _Slogans;
            }
        }

        public static string GetRandomSlogan()
        {
            var sloganCount = Slogans.Length;
            var random = new Random();
            var sloganIndex = random.Next(0, sloganCount);

            return Slogans[sloganIndex];
        }
    }
}