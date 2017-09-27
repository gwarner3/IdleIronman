namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateExerciseTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Run')");
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Bike')");
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Swim')");
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Row')");
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Spin')");
            Sql("INSERT INTO ExerciseTypeModels (Name) VALUES ('Water Aerobics')");
        }
        
        public override void Down()
        {
        }
    }
}
