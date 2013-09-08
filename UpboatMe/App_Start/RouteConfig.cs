using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WorldWideWat.SpriteThumbs;

namespace UpboatMe
{
    public class RouteConfig
    {
        public class ThumbsRouteHandler : MvcRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new ThumbsHttpHandler();
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "StaticPages",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { action = "Index|About|Terms|Privacy|Pricing" }
            );

            routes.MapRoute(
                name: "MemePages",
                url: "{action}/{*url}",
                defaults: new { controller = "Meme" },
                constraints: new { action = "Builder|Debug|List" }
                );

            routes.MapRoute(
                name: "Debug",
                url: "Debug/{top}/{bottom}",
                defaults: new { controller = "Meme", action = "Debug", top = UrlParameter.Optional, bottom = UrlParameter.Optional }
            );

            // http://stackoverflow.com/questions/6830796/regex-to-match-anything-but-two-words
            const string excludedMemeNames = "^(?!bundles|content|scripts).*";

            routes.MapRoute(
                name: "Meme Catch All",
                url: "{*url}",
                defaults: new { controller = "Meme", action = "Make" },
                constraints: new { url = excludedMemeNames }
            );
        }
    }
}