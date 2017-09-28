namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTeamNameColumnFromIdentityModels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "TeamName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TeamName", c => c.String(nullable: false));
        }
    }
}
