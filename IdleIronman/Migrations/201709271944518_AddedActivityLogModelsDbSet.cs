namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActivityLogModelsDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityDate = c.DateTime(nullable: false),
                        Distance = c.Double(),
                        DurationInMinutes = c.Int(),
                        ExerciseTypeModelsId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.ExerciseTypeModels", t => t.ExerciseTypeModelsId, cascadeDelete: true)
                .Index(t => t.ExerciseTypeModelsId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityLogModels", "ExerciseTypeModelsId", "dbo.ExerciseTypeModels");
            DropForeignKey("dbo.ActivityLogModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ActivityLogModels", new[] { "ApplicationUserId" });
            DropIndex("dbo.ActivityLogModels", new[] { "ExerciseTypeModelsId" });
            DropTable("dbo.ActivityLogModels");
        }
    }
}
