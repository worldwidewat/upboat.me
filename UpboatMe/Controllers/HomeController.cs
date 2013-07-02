using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UpboatMe.Models;
using System.Linq;

namespace UpboatMe.Controllers
{
    public class HomeController : Controller
    {
        // Note: don't forget to explicitly enable new actions in the route config
        public ActionResult Index()
        {
            var memes = GlobalMemeConfiguration.Memes.GetMemes();

            var viewModel = new IndexViewModel()
            {
                RecentMemes = new List<RecentMeme>
                {
                    new RecentMeme
                    {
                        Url = Url.Action("Make", "Meme", new { name = "sap", top= "wears-suit-and-tie-to-interview", bottom = "phone-interview"}),
                        Alt = "socially awkward penguin: wears suit and tie to interview" + Environment.NewLine + "phone interview",
                        Title = "socially awkward penguin"
                    },
                    new RecentMeme
                    {
                        Url = Url.Action("Make", "Meme", new { name = "fry", top= "not-sure-if-sunny-outside", bottom = "or-hungover"}),
                        Alt = "futurama fry: not sure if sunny outside" + Environment.NewLine + "or hungover",
                        Title = "futurama fry"
                    },
                    new RecentMeme
                    {
                        Url = Url.Action("Make", "Meme", new { name = "scc", top= "i-don't-buy-things-with-money", bottom = "i-buy-them-with-hours-of-my-life"}),
                        Alt = "sudden clarity clarence: i don't buy things with money" + Environment.NewLine + "i buy them with hours of my life",
                        Title = "sudden clarity clarence"
                    },
                    new RecentMeme
                    {
                        Url = Url.Action("Make", "Meme", new { name = "blb", top= "wears-suit-and-tie-to-interview", bottom = "phone-interview"}),
                        Alt = "bad luck brian: has pet rock" + Environment.NewLine + "it runs away",
                        Title = "bad luck brian"
                    }
                },
                BuilderViewModel = new BuilderViewModel 
                {
                    Memes = memes,
                    Top = "top-line",
                    Bottom = "bottom-line",
                    SelectedMeme = memes.First().Aliases.First()
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

        public ActionResult Privacy()
        {
            return View();
        }
    }
}
