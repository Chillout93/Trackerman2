using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackerman2.Core;
using Trackerman2.ViewModels;

namespace Trackerman2.Controllers
{
    public class HomeController : Controller
    {
        private MovieDbWrapper movieDb;

        public HomeController(MovieDbWrapper movieDb)
        {
            this.movieDb = movieDb;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Popular", "Show");
        }

        public void PopulateShows()
        {
            movieDb.PopulateNewTvShows();
        }
    }
}