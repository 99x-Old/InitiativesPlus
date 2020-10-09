using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InitiativesPlus.Infrastructure.Data.Migrations
{
    public partial class CreatedDateAndModifiedDateToInitiatives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Initiatives",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Initiatives",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Initiatives");
        }
    }
}
