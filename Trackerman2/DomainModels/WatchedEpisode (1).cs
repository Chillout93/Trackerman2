using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackerman2.Models;

namespace Trackerman2.DomainModels
{
    public class WatchedEpisode
    {
        public string UserID { get; set; }
        public int EpisodeID { get; set; }
        public bool Watched { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Episode Episode { get; set; }
    }
}