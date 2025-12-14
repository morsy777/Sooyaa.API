using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaURLProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Lessons");
        }
    }
}
