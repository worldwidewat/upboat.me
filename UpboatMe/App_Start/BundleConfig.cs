using System.Web.Optimization;

namespace UpboatMe
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //If your project requires jQuery, you may remove the zepto bundle
            bundles.Add(new ScriptBundle("~/bundles/zepto").Include(
                    "~/Scripts/zepto.js"));

            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                       "~/Content/foundation/foundation.css",
                       "~/Content/foundation/foundation.mvc.css",
                       "~/Content/foundation/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                      "~/Scripts/foundation/foundation.js",
                      "~/Scripts/foundation/foundation.*",
                      "~/Scripts/foundation/app.js"));

        }
    }
}