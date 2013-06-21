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
                name: "StaticPages",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { action = "Index|About|Terms|Privacy" }
            );

            routes.MapRoute(
                name: "Debug",
                url: "Debug/{top}/{bottom}",
                defaults: new { controller = "Meme", action = "Debug", top = UrlParameter.Optional, bottom = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Meme",
                url: "{name}/{top}/{bottom}",
                defaults: new { controller = "Meme", action = "Make", top = UrlParameter.Optional, bottom = UrlParameter.Optional }
            );
        }
    }
}