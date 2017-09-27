namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamApplicationModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamApplicationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationDate = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        WasDenied = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        TeamModelsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.TeamModels", t => t.TeamModelsId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.TeamModelsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamApplicationModels", "TeamModelsId", "dbo.TeamModels");
            DropForeignKey("dbo.TeamApplicationModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamApplicationModels", new[] { "TeamModelsId" });
            DropIndex("dbo.TeamApplicationModels", new[] { "ApplicationUserId" });
            DropTable("dbo.TeamApplicationModels");
        }
    }
}
