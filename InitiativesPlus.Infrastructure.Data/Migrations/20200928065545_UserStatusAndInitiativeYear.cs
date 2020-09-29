using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class UserStatusAndInitiativeYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.Id);
                });

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[UserStatuses] ON
                INSERT INTO [dbo].[UserStatuses] ([Id], [Status]) VALUES (1, N'Active')
                INSERT INTO [dbo].[UserStatuses] ([Id], [Status]) VALUES (2, N'Deactivated')
                INSERT INTO [dbo].[UserStatuses] ([Id], [Status]) VALUES (3, N'Deleted')
                SET IDENTITY_INSERT [dbo].[UserStatuses] OFF
            ");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Users",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "InitiativeYears",
                columns: table => new
                {
                    Year = table.Column<DateTime>(nullable: false),
                    InitiativeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeYears", x => new { x.Year, x.InitiativeId });
                    table.ForeignKey(
                        name: "FK_InitiativeYears_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeYears_InitiativeId",
                table: "InitiativeYears",
                column: "InitiativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserStatuses_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "UserStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserStatuses_StatusId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "InitiativeYears");

            migrationBuilder.DropTable(
                name: "UserStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatusId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Users");
        }
    }
}
