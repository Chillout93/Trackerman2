using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackerman2.DomainModels
{
    public class Episode
    {
        public int ID { get; set; }
        public DateTime? AirDate { get; set; }
        public int? EpisodeNumber { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public int? SeasonNumber { get; set; }
        public string StillPath { get; set; }
        public double? VoteAverage { get; set; }
        public double? VoteCount { get; set; }

        public virtual ICollection<WatchedEpisode> UserEpisodes { get; set; }
        public virtual Season Season { get; set; }

        public Episode()
        {
            this.UserEpisodes = new List<WatchedEpisode>();
            this.Season = new Season();
        }
    }
}