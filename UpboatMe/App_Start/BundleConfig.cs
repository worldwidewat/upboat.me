using System.Web.Optimization;

namespace UpboatMe
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // TODO: figure out how to make these two settings happen automatically in release builds...?
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;

            // css
            bundles.Add(new StyleBundle("~/Content/foundation/normalize", "//cdnjs.cloudflare.com/ajax/libs/foundation/4.1.6/css/normalize.min.css"));

            bundles.Add(new StyleBundle("~/Content/foundation/css",
                                        "//cdnjs.cloudflare.com/ajax/libs/foundation/4.1.6/css/foundation.min.css")
                            .Include("~/Content/foundation/foundation.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/styles.css"));

            // js
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                                         "//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js")
                            .Include("~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation",
                                         "//cdnjs.cloudflare.com/ajax/libs/foundation/4.1.6/js/foundation.min.js")
                            .Include("~/Scripts/foundation/foundation.js"));

            bundles.Add(new ScriptBundle("~/bundles/site")
                            .Include("~/Scripts/ZeroClipboard.js",
                                     "~/Scripts/site.js"));
        }
    }
}