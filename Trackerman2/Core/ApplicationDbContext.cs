using Microsoft.AspNet.Identity.EntityFramework;
using Trackerman2.Models;
using System.Data.Entity;
using Trackerman2.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackerman2.Core
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<UserShow> UserShows { get; set; }
        public DbSet<WatchedEpisode> WatchedEpisodes { get; set; }
        public DbSet<WatchedSeason> WatchedSeasons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>().Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Season>().Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Episode>().Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<UserShow>().HasKey(x => new { x.ShowID, x.UserID });
            modelBuilder.Entity<UserShow>().HasRequired(x => x.User).WithMany(x => x.Shows).WillCascadeOnDelete(false);

            modelBuilder.Entity<WatchedSeason>().HasKey(x => new { x.SeasonID, x.UserID });
            modelBuilder.Entity<WatchedSeason>().HasRequired(x => x.User).WithMany(x => x.Seasons).WillCascadeOnDelete(false);

            modelBuilder.Entity<WatchedEpisode>().HasKey(x => new { x.EpisodeID, x.UserID });
            modelBuilder.Entity<WatchedEpisode>().HasRequired(x => x.User).WithMany(x => x.Episodes).WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>().HasRequired(x => x.Show).WithMany(x => x.Seasons);
            modelBuilder.Entity<Episode>().HasRequired(x => x.Season).WithMany(x => x.Episodes);

            base.OnModelCreating(modelBuilder);
        }
    }
}