using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRanked.Migrations
{
    /// <inheritdoc />
    public partial class Update_quiz_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Quizzes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Quizzes");
        }
    }
}
