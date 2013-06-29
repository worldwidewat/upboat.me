using System;
using System.IO;
using System.Net;
using System.Web;
using UpboatMe.SpriteThumbs;

namespace UpboatMe.SpriteThumbs
{
    public class ThumbsHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var type = context.Request.QueryString["type"];

            if (string.Equals(type, "sprite", StringComparison.OrdinalIgnoreCase))
            {
                var bytes = File.ReadAllBytes(SpriteThumbsGlobalConfiguration.Configuration.SpriteFullFileName);
                
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
            }
            else if (string.Equals(type, "stylesheet", StringComparison.OrdinalIgnoreCase))
            {
                var bytes = File.ReadAllBytes(SpriteThumbsGlobalConfiguration.Configuration.StylesheetFullFileName);
                
                context.Response.ContentType = "text/css";
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }
}