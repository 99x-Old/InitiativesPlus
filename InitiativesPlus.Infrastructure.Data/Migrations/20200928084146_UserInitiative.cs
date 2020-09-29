using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class UserInitiative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInitiative",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    InitiativeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInitiative", x => new { x.UserId, x.InitiativeId });
                    table.ForeignKey(
                        name: "FK_UserInitiative_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInitiative_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiative_InitiativeId",
                table: "UserInitiative",
                column: "InitiativeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInitiative");
        }
    }
}
