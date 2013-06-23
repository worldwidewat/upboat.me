using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UpboatMe.Models;

namespace UpboatMe.Controllers
{
    public class HomeController : Controller
    {
        // Note: don't forget to explicitly enable new actions in the route config
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                Memes = GlobalMemeConfiguration.Memes.GetMemes(),
                RecentMemes = new List<RecentMeme>
                {
                    new RecentMeme
                    {
                        Url = "/sap/wears-suit-and-tie-to-interview/phone-interview",
                        Alt = "socially awkward penguin: wears suit and tie to interview" + Environment.NewLine + "phone interview",
                        Title = "socially awkward penguin"
                    },
                    new RecentMeme
                    {
                        Url = "/fry/not-sure-if-sunny-outside/or-hungover",
                        Alt = "futurama fry: not sure if sunny outside" + Environment.NewLine + "or hungover",
                        Title = "futurama fry"
                    },
                    new RecentMeme
                    {
                        Url = "/scc/i-don't-buy-things-with-money/i-buy-them-with-hours-of-my-life",
                        Alt = "sudden clarity clarence: i don't buy things with money" + Environment.NewLine + "i buy them with hours of my life",
                        Title = "sudden clarity clarence"
                    },
                    new RecentMeme
                    {
                        Url = "/blb/has-pet-rock/it-runs-away",
                        Alt = "bad luck brian: has pet rock" + Environment.NewLine + "it runs away",
                        Title = "bad luck brian"
                    }
                }
            };

            return View(viewModel);
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Pricing()
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
