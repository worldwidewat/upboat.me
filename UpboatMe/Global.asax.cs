using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UpboatMe.App_Start;
using UpboatMe.Models;
using UpboatMe.SpriteThumbs;

namespace UpboatMe
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            var filepaths = Directory.GetFiles(HttpContext.Current.Server.MapPath(@"~\Images"), "*.png");
            var filenames = filepaths.Select(f => Path.GetFileName(f));
            MemeConfig.AutoRegisterMemesByFile(GlobalMemeConfiguration.Memes, filenames.ToArray());
            MemeConfig.RegisterManualMemes(GlobalMemeConfiguration.Memes);

            SpriteThumbsConfig.Initialize(SpriteThumbsGlobalConfiguration.Configuration);

            var generator = new SpriteThumbsGenerator(SpriteThumbsGlobalConfiguration.Configuration);

            generator.Generate();
        }
    }
}