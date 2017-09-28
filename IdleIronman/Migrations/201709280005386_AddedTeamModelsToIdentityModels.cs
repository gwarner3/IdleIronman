namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamModelsToIdentityModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TeamModelsId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "TeamModelsId");
            AddForeignKey("dbo.AspNetUsers", "TeamModelsId", "dbo.TeamModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "TeamModelsId", "dbo.TeamModels");
            DropIndex("dbo.AspNetUsers", new[] { "TeamModelsId" });
            DropColumn("dbo.AspNetUsers", "TeamModelsId");
        }
    }
}
