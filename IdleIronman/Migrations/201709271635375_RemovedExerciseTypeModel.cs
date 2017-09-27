namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedExerciseTypeModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ExerciseModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExerciseModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
