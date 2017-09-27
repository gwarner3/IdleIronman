namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateExereciseType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Run')");
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Bike')");
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Swim')");
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Row')");
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Spin')");
            Sql("INSERT INTO ExerciseModels (Name) VALUES ('Water Aerobics')");
        }
        
        public override void Down()
        {
        }
    }
}
