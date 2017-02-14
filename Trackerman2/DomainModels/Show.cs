using System.Collections.Generic;
using System.Linq;

namespace Trackerman2.DomainModels
{
    public class Show
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }
        public string FirstAirDate { get; set; }
        public double? Popularity { get; set; }
        public double? VoteAverage { get; set; }
        public int? VoteCount { get; set; }

        public ICollection<Season> Seasons { get; set; }
        public ICollection<UserShow> UserShows { get; set; }

        public Show()
        {
            this.Seasons = new List<Season>();
            this.UserShows = new List<UserShow>();
        }

        public Episode GetLatestEpisode(string userID)
        {
            var latestSeason = this.Seasons.OrderBy(x => x.SeasonNumber).FirstOrDefault(x => x.UserSeasons?.Where(y => y.UserID == userID && y.Watched == true).Count() == 0);
            if (latestSeason == null)
                return null;

            return latestSeason.Episodes.OrderBy(x => x.EpisodeNumber).FirstOrDefault(x => x.UserEpisodes?.Where(y => y.UserID == userID && y.Watched == true).Count() == 0);
        }
    }
}