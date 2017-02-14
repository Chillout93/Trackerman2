using System.Collections.Generic;

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
    }
}