namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrivacyPropertyToTeamModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamModels", "IsPrivate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamModels", "IsPrivate");
        }
    }
}
