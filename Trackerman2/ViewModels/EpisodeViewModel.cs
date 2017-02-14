using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackerman2.ViewModels
{
    public class EpisodeViewModel
    {
        public int ID { get; set; }
        public string AirDate { get; set; }
        public int? EpisodeNumber { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string StillPath { get; set; }
        public bool Watched { get; set; }

        public EpisodeViewModel(int id, int episodeNumber, string name, string overview, string stillPath, bool watched, DateTime? airDate = null)
        {
            this.ID = id;
            this.EpisodeNumber = episodeNumber;
            this.Name = name;
            this.Overview = overview;
            this.StillPath = stillPath;
            this.Watched = watched;
            this.AirDate = airDate.HasValue ? airDate.Value.ToString("dd MMM yyyy") : "";
        }
    }
}