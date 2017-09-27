namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        LinkToPhoto = c.String(),
                        IronManRuleModelsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IronManRuleModels", t => t.IronManRuleModelsId, cascadeDelete: true)
                .Index(t => t.IronManRuleModelsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamModels", "IronManRuleModelsId", "dbo.IronManRuleModels");
            DropIndex("dbo.TeamModels", new[] { "IronManRuleModelsId" });
            DropTable("dbo.TeamModels");
        }
    }
}
