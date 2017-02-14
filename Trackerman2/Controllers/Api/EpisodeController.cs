using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trackerman2.Core;

namespace Trackerman2.Controllers.Api
{
    public class EpisodeController : ApiController
    {
        private ApplicationDbContext db;

        public EpisodeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IHttpActionResult WatchEpisode([FromBody]int episodeID)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Include(x => x.Episodes).Include(x => x.Seasons.Select(y => y.Season.Show)).Single(x => x.Id == userID);
            var episode = db.Episodes.Include(x => x.Season.Show).Include(x => x.Season.Episodes).Single(x => x.ID == episodeID);

            user.WatchEpisode(episode);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnwatchEpisode([FromUri]int id)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users
                .Include(x => x.Seasons.Select(y => y.Season.Episodes))
                .Include(x => x.Seasons.Select(y => y.Season.Show))
                .Include(x => x.Episodes)
                .Single(x => x.Id == userID);

            user.UnwatchEpisode(id);
            db.SaveChanges();

            return Ok();
        }
    }
}
