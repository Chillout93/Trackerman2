namespace Trackerman2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSeasonAndEpisodeRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WatchedEpisodes",
                c => new
                    {
                        EpisodeID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Watched = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.EpisodeID, t.UserID })
                .ForeignKey("dbo.Episodes", t => t.EpisodeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.EpisodeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.WatchedSeasons",
                c => new
                    {
                        SeasonID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Watched = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeasonID, t.UserID })
                .ForeignKey("dbo.Seasons", t => t.SeasonID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.SeasonID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WatchedSeasons", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.WatchedSeasons", "SeasonID", "dbo.Seasons");
            DropForeignKey("dbo.WatchedEpisodes", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.WatchedEpisodes", "EpisodeID", "dbo.Episodes");
            DropIndex("dbo.WatchedSeasons", new[] { "UserID" });
            DropIndex("dbo.WatchedSeasons", new[] { "SeasonID" });
            DropIndex("dbo.WatchedEpisodes", new[] { "UserID" });
            DropIndex("dbo.WatchedEpisodes", new[] { "EpisodeID" });
            DropTable("dbo.WatchedSeasons");
            DropTable("dbo.WatchedEpisodes");
        }
    }
}
