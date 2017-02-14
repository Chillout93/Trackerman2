using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackerman2.Core;
using Trackerman2.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Trackerman2.DomainModels;

namespace Trackerman2.Controllers
{
    public class ShowController : Controller
    {
        private ApplicationDbContext db;

        public ShowController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ActionResult Details(int id)
        {
            var userID = User.Identity.GetUserId();
            var show = db.Shows
                .Include(x => x.Seasons.Select(y => y.Episodes.Select(z => z.UserEpisodes)))
                .Include(x => x.Seasons.Select(y => y.UserSeasons))
                .Include(x => x.UserShows)
                .Single(x => x.ID == id);

            
            var showViewModel = new ShowViewModel(show.ID, show.Name, show.Overview, show.UserShows.Any(x => x.UserID == userID && x.Tracking), GetLatestEpisode(show, userID), show.PosterPath, show.FirstAirDate, show.Seasons.Select(x => new SeasonViewModel(x.ID, x.Name, x.PosterPath, x.UserSeasons.Any(y => y.UserID == userID && y.Watched), x.SeasonNumber.Value, x.Episodes.Select(y => new EpisodeViewModel(y.ID, y.EpisodeNumber.Value, y.Name, y.Overview, y.StillPath, y.UserEpisodes.Any(z => z.UserID == userID && z.Watched), y.AirDate)))));

            return View(showViewModel);
        }

        public ActionResult Popular()
        {
            var user = User.Identity.GetUserId();

            var shows = db.Shows
                .Include(x => x.UserShows)
                .OrderByDescending(x => x.Popularity)
                .Take(50)
                .ToList()
                .Select(x => new ShowViewModel(x.ID, x.Name, x.Overview, x.UserShows.Any(y => y.UserID == user && y.Tracking), null, x.PosterPath, x.FirstAirDate));

            return View(new ShowIndexViewModel() { Shows = shows });
        }

        public ActionResult UserShows()
        {
            var userID = User.Identity.GetUserId();

            var shows = db.UserShows
                .Where(x => x.UserID == userID && x.Tracking)
                .Include(x => x.Show.Seasons.Select(y => y.UserSeasons))
                .Include(x => x.Show.Seasons.Select(y => y.Episodes.Select(z => z.UserEpisodes)))
                .ToList()
                .Select(x => new ShowViewModel(x.Show.ID, x.Show.Name, x.Show.Overview, true, GetLatestEpisode(x.Show, userID), x.Show.PosterPath, x.Show.FirstAirDate));


            return View(new ShowIndexViewModel() { Shows = shows });
        }

        public ActionResult Search(string query = "")
        {
            var user = User.Identity.GetUserId();

            var shows = db.Shows
                .Include(x => x.UserShows)
                .Where(x => x.Name.Contains(query))
                .Take(10)
                .ToList()
                .Select(x => new ShowViewModel(x.ID, x.Name, x.Overview, x.UserShows.Any(y => y.UserID == user && y.Tracking), null, x.PosterPath, x.FirstAirDate));

            var showIndexViewModel = new ShowSearchViewModel() { Shows = shows, SearchTerm = query };
            return View(showIndexViewModel);
        }

        private EpisodeViewModel GetLatestEpisode(Show show, string userID)
        {
            var latestEpisode = show.GetLatestEpisode(userID);
            return (latestEpisode == null) ? null : 
                new EpisodeViewModel(latestEpisode.ID, latestEpisode.EpisodeNumber.Value, latestEpisode.Name, latestEpisode.Overview, latestEpisode.StillPath, false, latestEpisode.AirDate);
        }
    }
}