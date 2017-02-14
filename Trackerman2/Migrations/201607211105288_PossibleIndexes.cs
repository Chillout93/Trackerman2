namespace Trackerman2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PossibleIndexes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Episodes", "Season_ID", "dbo.Seasons");
            DropForeignKey("dbo.Seasons", "Show_ID", "dbo.Shows");
            DropIndex("dbo.Episodes", new[] { "Season_ID" });
            DropIndex("dbo.Seasons", new[] { "Show_ID" });
            AlterColumn("dbo.Episodes", "Season_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Seasons", "Show_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodes", "Season_ID");
            CreateIndex("dbo.Seasons", "Show_ID");
            AddForeignKey("dbo.Episodes", "Season_ID", "dbo.Seasons", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Seasons", "Show_ID", "dbo.Shows", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "Show_ID", "dbo.Shows");
            DropForeignKey("dbo.Episodes", "Season_ID", "dbo.Seasons");
            DropIndex("dbo.Seasons", new[] { "Show_ID" });
            DropIndex("dbo.Episodes", new[] { "Season_ID" });
            AlterColumn("dbo.Seasons", "Show_ID", c => c.Int());
            AlterColumn("dbo.Episodes", "Season_ID", c => c.Int());
            CreateIndex("dbo.Seasons", "Show_ID");
            CreateIndex("dbo.Episodes", "Season_ID");
            AddForeignKey("dbo.Seasons", "Show_ID", "dbo.Shows", "ID");
            AddForeignKey("dbo.Episodes", "Season_ID", "dbo.Seasons", "ID");
        }
    }
}
