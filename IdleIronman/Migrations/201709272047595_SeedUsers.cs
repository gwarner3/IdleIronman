namespace IdleIronman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5883f16f-bc28-4b5e-ad30-006a5e830923', N'tc@iim.com', 0, N'AP05PgGjgcBmHB0A2ls7E4+XWJAJJUyXHiC7GmRS8CLkzdcHUfP/0Lx4RyE5urwPTA==', N'65aa4e78-27b2-4478-992d-441aaf7af289', NULL, 0, 0, NULL, 1, 0, N'tc@iim.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8bc15c5f-f2f9-4802-9da8-11b679c4c446', N'p@iim.com', 0, N'ABRc8/9tOJj6FRIkT0aoJqRG8mShOqnHKyNsSICU8JnL+ciMv28rww6q1e9BvtViuw==', N'89ee5dac-cb41-43f4-86a2-d0af21539092', NULL, 0, 0, NULL, 1, 0, N'p@iim.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ad7ca756-e617-4dae-a7f6-68254b47d2a0', N'admin@iim.com', 0, N'APXPaXTd8p0fTjyFS+D74b2CgbsmUwkC1P1oyLTn4QslFvG4o7szoD61Z1RzTyZqBw==', N'21d21888-2951-48f4-8879-38691e71ce36', NULL, 0, 0, NULL, 1, 0, N'admin@iim.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'87579e46-4ad2-45dc-bcc5-8447b84bf079', N'CanManageAllData')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'429de1c6-97d7-401c-b374-50a5b47bfad9', N'CanManagePersonalData')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'07593dae-64f7-4f02-b522-9a72bbccb2ba', N'CanManageTeamData')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5883f16f-bc28-4b5e-ad30-006a5e830923', N'07593dae-64f7-4f02-b522-9a72bbccb2ba')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8bc15c5f-f2f9-4802-9da8-11b679c4c446', N'429de1c6-97d7-401c-b374-50a5b47bfad9')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ad7ca756-e617-4dae-a7f6-68254b47d2a0', N'87579e46-4ad2-45dc-bcc5-8447b84bf079')

");
        }
        
        public override void Down()
        {
        }
    }
}
