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
    public class ShowController : ApiController
    {
        private ApplicationDbContext db;

        public ShowController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IHttpActionResult TrackShow([FromBody]int showID)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Include(x => x.Shows).Single(x => x.Id == userID);

            user.TrackShow(showID);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UntrackShow([FromUri]int id)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Include(x => x.Shows).Single(x => x.Id == userID);

            user.UnTrackShow(id);
            db.SaveChanges();

            return Ok();
        }
    }
}
