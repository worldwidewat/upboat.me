using System;
using System.Web;

namespace UpboatMe.Modules
{
    // via http://arturito.net/2011/10/21/how-to-remove-server-x-aspnet-version-x-aspnetmvc-version-and-x-powered-by-from-the-response-header-in-iis7/
    public class HideServerHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Set("Server", "Memeverse/1.0");
        }
    }
}