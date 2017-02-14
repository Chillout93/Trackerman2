namespace Trackerman2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialShowModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AirDate = c.DateTime(),
                        EpisodeNumber = c.Int(),
                        Name = c.String(),
                        Overview = c.String(),
                        SeasonNumber = c.Int(),
                        StillPath = c.String(),
                        VoteAverage = c.Double(),
                        VoteCount = c.Double(),
                        Season_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Seasons", t => t.Season_ID)
                .Index(t => t.Season_ID);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Overview = c.String(),
                        AirDate = c.DateTime(),
                        EpisodeCount = c.Int(),
                        PosterPath = c.String(),
                        SeasonNumber = c.Int(),
                        Show_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Shows", t => t.Show_ID)
                .Index(t => t.Show_ID);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Overview = c.String(),
                        BackdropPath = c.String(),
                        PosterPath = c.String(),
                        FirstAirDate = c.String(),
                        Popularity = c.Double(),
                        VoteAverage = c.Double(),
                        VoteCount = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "Show_ID", "dbo.Shows");
            DropForeignKey("dbo.Episodes", "Season_ID", "dbo.Seasons");
            DropIndex("dbo.Seasons", new[] { "Show_ID" });
            DropIndex("dbo.Episodes", new[] { "Season_ID" });
            DropTable("dbo.Shows");
            DropTable("dbo.Seasons");
            DropTable("dbo.Episodes");
        }
    }
}
