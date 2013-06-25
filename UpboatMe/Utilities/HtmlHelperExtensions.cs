using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

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
    }
}