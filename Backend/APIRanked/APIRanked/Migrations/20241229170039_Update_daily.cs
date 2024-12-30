using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRanked.Migrations
{
    /// <inheritdoc />
    public partial class Update_daily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dailies",
                table: "Dailies");

            migrationBuilder.DropIndex(
                name: "IX_Dailies_TypeId",
                table: "Dailies");

            migrationBuilder.DropColumn(
                name: "DailyId",
                table: "Dailies");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Dailies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dailies",
                table: "Dailies",
                columns: new[] { "TypeId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dailies",
                table: "Dailies");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Dailies");

            migrationBuilder.AddColumn<int>(
                name: "DailyId",
                table: "Dailies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dailies",
                table: "Dailies",
                column: "DailyId");

            migrationBuilder.CreateIndex(
                name: "IX_Dailies_TypeId",
                table: "Dailies",
                column: "TypeId");
        }
    }
}
