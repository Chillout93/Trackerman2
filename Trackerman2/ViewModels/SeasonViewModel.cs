using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackerman2.ViewModels
{
    public class SeasonViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PosterPath { get; set; }
        public int SeasonNumber { get; set; }
        public bool Watched { get; set; }

        public IEnumerable<EpisodeViewModel> Episodes { get; set; }

        public SeasonViewModel(int id, string name, string posterPath, bool watched, int seasonNumber = 0, IEnumerable<EpisodeViewModel> episodes = null)
        {
            this.ID = id;
            this.Name = name;
            this.PosterPath = posterPath;
            this.SeasonNumber = seasonNumber;
            this.Watched = watched;
            this.Episodes = episodes ?? new List<EpisodeViewModel>();
            this.Episodes = this.Episodes.OrderBy(x => x.EpisodeNumber);
        }
    }
}