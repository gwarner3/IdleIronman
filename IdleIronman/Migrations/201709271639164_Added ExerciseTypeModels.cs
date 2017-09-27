namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExerciseTypeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseTypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExerciseTypeModels");
        }
    }
}
