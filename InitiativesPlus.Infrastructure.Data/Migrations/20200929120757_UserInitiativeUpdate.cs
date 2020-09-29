using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class UserInitiativeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiative_Initiatives_InitiativeId",
                table: "UserInitiative");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiative_Users_UserId",
                table: "UserInitiative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInitiative",
                table: "UserInitiative");

            migrationBuilder.RenameTable(
                name: "UserInitiative",
                newName: "UserInitiatives");

            migrationBuilder.RenameIndex(
                name: "IX_UserInitiative_InitiativeId",
                table: "UserInitiatives",
                newName: "IX_UserInitiatives_InitiativeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInitiatives",
                table: "UserInitiatives",
                columns: new[] { "UserId", "InitiativeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_Initiatives_InitiativeId",
                table: "UserInitiatives",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_Users_UserId",
                table: "UserInitiatives",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_Initiatives_InitiativeId",
                table: "UserInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_Users_UserId",
                table: "UserInitiatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInitiatives",
                table: "UserInitiatives");

            migrationBuilder.RenameTable(
                name: "UserInitiatives",
                newName: "UserInitiative");

            migrationBuilder.RenameIndex(
                name: "IX_UserInitiatives_InitiativeId",
                table: "UserInitiative",
                newName: "IX_UserInitiative_InitiativeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInitiative",
                table: "UserInitiative",
                columns: new[] { "UserId", "InitiativeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiative_Initiatives_InitiativeId",
                table: "UserInitiative",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiative_Users_UserId",
                table: "UserInitiative",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
