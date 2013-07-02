using System;
using System.Web.Mvc;

namespace UpboatMe.Utilities
{
    public static class UrlHelperExtensionMethods
    {
        public static string AbsoluteAction(this UrlHelper url, string path)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = string.Format("{0}://{1}{2}",
                                                  requestUrl.Scheme,
                                                  requestUrl.Authority,
                                                  path);

            return absoluteAction;
        }
    }
}