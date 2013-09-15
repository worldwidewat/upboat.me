using System.IO;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;
using WorldWideWat.SpriteThumbs;
using System.Web;
using System.Web.Hosting;

namespace UpboatMe.Utilities
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TopNavLink(this HtmlHelper helper, string text, string action, string controller, object routeValues, object htmlAttributes)
        {
            var isCurrent = 
                string.Equals(helper.ViewContext.RouteData.Values["controller"].ToString(), controller, System.StringComparison.OrdinalIgnoreCase) &&
                string.Equals(helper.ViewContext.RouteData.Values["action"].ToString(), action, System.StringComparison.OrdinalIgnoreCase);

            
            var htmlAttributesCollection = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new object());

            if (isCurrent)
            {
                if (htmlAttributesCollection.ContainsKey("class"))
                {
                    htmlAttributesCollection["class"] = htmlAttributesCollection["class"] + " success";
                }
                else
                {
                    htmlAttributesCollection.Add("class", "success");
                }
            }

            var routeValuesDictionary = new RouteValueDictionary(routeValues);
            IDictionary<string, object> attributesDictionary = htmlAttributesCollection.ToDictionary(k => k.Key, v => v.Value);

            return helper.ActionLink(text, action, controller, routeValuesDictionary, attributesDictionary);
        }

        public static MvcHtmlString ThumbImage(this HtmlHelper helper, string imageNameWithoutExtension)
        {
            var builder = new TagBuilder("div");

            builder.AddCssClass(SpriteThumbsConfiguration.GetThumbClassName());
            builder.AddCssClass(SpriteThumbsConfiguration.GetImageClassName(imageNameWithoutExtension));

            return new MvcHtmlString(builder.ToString());
        }

        public static string LastUpdated(this HtmlHelper helper)
        {
            var filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "version.txt");
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return "";
        }
    }
}