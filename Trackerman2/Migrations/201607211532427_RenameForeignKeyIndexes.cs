namespace Trackerman2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameForeignKeyIndexes : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Episodes", name: "Season_ID", newName: "SeasonID");
            RenameColumn(table: "dbo.Seasons", name: "Show_ID", newName: "ShowID");
            RenameIndex(table: "dbo.Episodes", name: "IX_Season_ID", newName: "IX_SeasonID");
            RenameIndex(table: "dbo.Seasons", name: "IX_Show_ID", newName: "IX_ShowID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Seasons", name: "IX_ShowID", newName: "IX_Show_ID");
            RenameIndex(table: "dbo.Episodes", name: "IX_SeasonID", newName: "IX_Season_ID");
            RenameColumn(table: "dbo.Seasons", name: "ShowID", newName: "Show_ID");
            RenameColumn(table: "dbo.Episodes", name: "SeasonID", newName: "Season_ID");
        }
    }
}
