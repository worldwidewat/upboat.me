using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UpboatMe.App_Start;
using UpboatMe.Models;

namespace UpboatMe
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var filepaths = Directory.GetFiles(HttpContext.Current.Server.MapPath(@"~\Images"), "*.jpg");
            var filenames = filepaths.Select(f => Path.GetFileName(f));
            
            MemeConfig.AutoRegisterMemesByFile(GlobalMemeConfiguration.Memes, filenames.ToArray());
            MemeConfig.RegisterManualMemes(GlobalMemeConfiguration.Memes);

            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}