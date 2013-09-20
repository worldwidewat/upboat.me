using System;
using System.IO;
using System.Web.Mvc;

namespace UpboatMe.Utilities
{
    public static class UrlHelperExtensionMethods
    {
        public static string AbsoluteAction(this UrlHelper url, string path)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;

            var sslHeaderValue = url.RequestContext.HttpContext.Request.Headers["X-FORWARDED-PROTO"];
            var isSsl = url.RequestContext.HttpContext.Request.IsSecureConnection ||
                        string.Equals(sslHeaderValue, "https", StringComparison.OrdinalIgnoreCase);

            string absoluteAction = string.Format("{0}://{1}{2}",
                                                  isSsl ? "https" : "http",
                                                  requestUrl.Authority,
                                                  path);

            return absoluteAction;
        }

        public static string VersionedContent(this UrlHelper url, string path)
        {
            var filepath = url.RequestContext.HttpContext.Server.MapPath(path);
            var lastWriteTime = File.GetLastWriteTimeUtc(filepath);
            var cacheBuster = lastWriteTime.Ticks;

            return string.Format("{0}?v={1}", url.Content(path), cacheBuster);
        }
    }
}