namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIronManRuleModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IronManRuleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        DurationInDays = c.Int(nullable: false, defaultValue: 30),
                        SwimDistanceInMiles = c.Double(nullable: false, defaultValue: 2.4),
                        BikeDistancenIMiles = c.Double(nullable: false, defaultValue: 112),
                        RunDistanceInMiles = c.Double(nullable: false, defaultValue: 26.2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IronManRuleModels");
        }
    }
}
