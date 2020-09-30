using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class InitiativeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitiativeStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeStatuses", x => x.Id);
                });

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[InitiativeStatuses] ON
                INSERT INTO [dbo].[InitiativeStatuses] ([Id], [Status]) VALUES (1, N'Active')
                INSERT INTO [dbo].[InitiativeStatuses] ([Id], [Status]) VALUES (2, N'Inactive')
                SET IDENTITY_INSERT [dbo].[InitiativeStatuses] OFF
            ");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Initiatives",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_StatusId",
                table: "Initiatives",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_StatusId",
                table: "Initiatives",
                column: "StatusId",
                principalTable: "InitiativeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_StatusId",
                table: "Initiatives");

            migrationBuilder.DropTable(
                name: "InitiativeStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Initiatives_StatusId",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Initiatives");
        }
    }
}
