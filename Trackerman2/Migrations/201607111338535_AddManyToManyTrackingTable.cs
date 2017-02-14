namespace Trackerman2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyTrackingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserShows",
                c => new
                    {
                        ShowID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Tracking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShowID, t.UserID })
                .ForeignKey("dbo.Shows", t => t.ShowID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ShowID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserShows", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserShows", "ShowID", "dbo.Shows");
            DropIndex("dbo.UserShows", new[] { "UserID" });
            DropIndex("dbo.UserShows", new[] { "ShowID" });
            DropTable("dbo.UserShows");
        }
    }
}
