using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UpboatMe.Controllers
{
    public class HomeController : Controller
    {
        // Note: don't forget to explicitly enable new actions in the route config
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}
