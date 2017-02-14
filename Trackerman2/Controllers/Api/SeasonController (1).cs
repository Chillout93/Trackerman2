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
    public class SeasonController : ApiController
    {
        private ApplicationDbContext db;

        public SeasonController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IHttpActionResult WatchSeason([FromBody]int seasonID)
        {
            var userID = User.Identity.GetUserId();
            var season = db.Seasons.Include(x => x.Episodes).Single(x => x.ID == seasonID);
            var user = db.Users
                .Include(x => x.Seasons.Select(y => y.Season.Episodes))
                .Include(x => x.Episodes)
                .Single(x => x.Id == userID);

            user.WatchSeason(season);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnwatchSeason([FromUri]int id)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users
                .Include(x => x.Seasons.Select(y => y.Season.Episodes))
                .Include(x => x.Seasons.Select(y => y.Season.UserSeasons))
                .Single(x => x.Id == userID);

            user.UnwatchSeason(id);
            db.SaveChanges();

            return Ok();
        }
    }
}
