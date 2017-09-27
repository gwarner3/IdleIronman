namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIronManRuleTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.IronManRuleModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IronManRuleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SwimDistanceInMiles = c.Double(nullable: false),
                        BikeDistancenIMiles = c.Double(nullable: false),
                        RunDistanceInMiles = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
