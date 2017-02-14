using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trackerman2.DomainModels;

namespace Trackerman2.ViewModels
{
    public class ShowViewModel
    {
        private DateTime firstAirDate;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public int FirstAirDateYear { get; set; }
        public bool Tracking { get; set; }
        public IEnumerable<SeasonViewModel> Seasons { get; set; }
        public EpisodeViewModel LatestEpisode { get; set; }

        public ShowViewModel(int id, string name, string overview, bool tracking, EpisodeViewModel latestEpisode = null, string posterPath = "", string firstAirDate = "", IEnumerable<SeasonViewModel> seasons = null)
        {
            this.ID = id;
            this.Name = name;
            this.Overview = overview;
            this.PosterPath = posterPath;
            this.FirstAirDateYear = (DateTime.TryParse(firstAirDate, out this.firstAirDate)) ? this.firstAirDate.Year : 0;
            this.Tracking = tracking;
            this.LatestEpisode = latestEpisode;

            this.Seasons = seasons ?? new List<SeasonViewModel>();
            this.Seasons = this.Seasons.OrderBy(x => x.SeasonNumber);
        }
    }
}