using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackerman2.DomainModels
{
    public class Season
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }
        public int? EpisodeCount { get; set; }
        public string PosterPath { get; set; }
        public int? SeasonNumber { get; set; }

        public ICollection<Episode> Episodes { get; set; }
        public ICollection<WatchedSeason> UserSeasons { get; set; }

        public Show Show { get; set; }

        public Season()
        {
            Show = new Show();
            Episodes = new List<Episode>();
            UserSeasons = new List<WatchedSeason>();
        }
    }
}