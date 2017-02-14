using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trackerman2.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace Trackerman2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserShow> Shows { get; set; }
        public ICollection<WatchedSeason> Seasons { get; set; }
        public ICollection<WatchedEpisode> Episodes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public void TrackShow(int showID)
        {
            var show = Shows.SingleOrDefault(x => x.ShowID == showID);

            if (show == null)
                Shows.Add(new UserShow() { ShowID = showID, UserID = this.Id, Tracking = true });
            else
                show.Tracking = true;
        }

        public void UnTrackShow(int showID)
        {
            Shows.Single(x => x.ShowID == showID).Tracking = false;
        }

        public void WatchSeason(Season season)
        {
            var userSeason = Seasons.SingleOrDefault(x => x.SeasonID == season.ID);

            if (userSeason == null)
            {
                userSeason = new WatchedSeason() { SeasonID = season.ID, UserID = this.Id, Watched = true };
                Seasons.Add(userSeason);
            }
            else
                userSeason.Watched = true;

            foreach (var episode in season.Episodes)
            {
                var userEpisode = Episodes.FirstOrDefault(x => x.EpisodeID == episode.ID);
                if (userEpisode == null)
                    Episodes.Add(new WatchedEpisode() { EpisodeID = episode.ID, UserID = this.Id, Watched = true });
                else
                    userEpisode.Watched = true;
            }
        }

        public void UnwatchSeason(int seasonID)
        {
            var season = Seasons.Single(x => x.SeasonID == seasonID);
            season.Watched = false;
            foreach (var episode in season.Season.Episodes.Select(x => x.UserEpisodes.Where(y => y.UserID == this.Id)))
                foreach (var ep in episode)
                    ep.Watched = false;   
        }

        public void WatchEpisode(Episode episode)
        {
            var userEpisode = Episodes.SingleOrDefault(x => x.EpisodeID == episode.ID);
            
            if (userEpisode == null)
                Episodes.Add(new WatchedEpisode() { EpisodeID = episode.ID, UserID = this.Id, Watched = true });
            else
                userEpisode.Watched = true;

            if (!episode.Season.Episodes.Any(x => !Episodes.Any(y => y.EpisodeID == x.ID && y.Watched == true)))
            {
                var userSeason = Seasons.SingleOrDefault(x => x.SeasonID == episode.Season.ID);
                if (userSeason == null)
                    Seasons.Add(new WatchedSeason() { SeasonID = episode.Season.ID, UserID = this.Id });
                else
                    userSeason.Watched = true;
            } 
        }

        public void UnwatchEpisode(int episodeID)
        {
            var episode = Episodes.Single(x => x.EpisodeID == episodeID);
            episode.Watched = false;

            var season = episode.Episode.Season;
            season.UserSeasons.Single(x => x.UserID == this.Id).Watched = true;
            foreach (var seasonEpisode in season.Episodes)
                if (seasonEpisode.UserEpisodes.Single(x => x.UserID == this.Id).Watched == false)
                {
                    season.UserSeasons.Single(x => x.UserID == this.Id).Watched = false;
                    return;
                }
        }
    }
}