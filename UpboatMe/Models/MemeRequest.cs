using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using UpboatMe.Utilities;

namespace UpboatMe.Models
{
    public class MemeRequest
    {
        public string Name { get; set; }
        public string Top { get; set; }
        public string Bottom { get; set; }
        public bool IsDebugMode { get; set; }

        public MemeRequest()
        {
            Name = "";
            Top = "";
            Bottom = "";
        }

        private static Regex _StripRegex = new Regex(@"&?debugmode=true|\.png$|\.jpe?g$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static MemeRequest FromUrl(string url, HttpServerUtilityBase serverUtility)
        {
            // decode any %-encodings
            url = serverUtility.UrlDecode(url) ?? "";
            
            // decode html entities, e.g. &gt;
            // Note: this would technically work for entites with a hash sign, e.g. &#39;, _but_
            // the browser doesn't send anything after the hash sign because it's part of the url
            // fragment...so we're sol when it comes to those
            url = WebUtility.HtmlDecode(url); 

            // strip off any file extensions
            var isDebugMode = url.Contains("debugMode=true");
            url = _StripRegex.Replace(url, "");

            var memeRequest = MemeUtilities.GetMemeRequest(url);
            memeRequest.IsDebugMode = isDebugMode;

            return memeRequest;
        }
    }
}