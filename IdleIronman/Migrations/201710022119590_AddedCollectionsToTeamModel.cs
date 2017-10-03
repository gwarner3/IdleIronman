namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCollectionsToTeamModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityLogModels", "TeamModels_Id", c => c.Int());
            CreateIndex("dbo.ActivityLogModels", "TeamModels_Id");
            AddForeignKey("dbo.ActivityLogModels", "TeamModels_Id", "dbo.TeamModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityLogModels", "TeamModels_Id", "dbo.TeamModels");
            DropIndex("dbo.ActivityLogModels", new[] { "TeamModels_Id" });
            DropColumn("dbo.ActivityLogModels", "TeamModels_Id");
        }
    }
}
