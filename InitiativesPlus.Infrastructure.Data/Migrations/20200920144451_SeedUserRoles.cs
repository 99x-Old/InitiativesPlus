using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[UserRoles] ON
                INSERT INTO [dbo].[UserRoles] ([Id], [RoleName]) VALUES (1, N'User')
                INSERT INTO [dbo].[UserRoles] ([Id], [RoleName]) VALUES (2, N'Initiative Lead')
                INSERT INTO [dbo].[UserRoles] ([Id], [RoleName]) VALUES (3, N'Super Admin')
                INSERT INTO [dbo].[UserRoles] ([Id], [RoleName]) VALUES (4, N'Initiative Evaluator')
                SET IDENTITY_INSERT [dbo].[UserRoles] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
