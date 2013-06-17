using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UpboatMe
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Meme",
                url: "{meme}/{first}/{last}",
                defaults: new { controller = "Meme", action = "Make", first = UrlParameter.Optional, last = UrlParameter.Optional }
            );
        }
    }
}