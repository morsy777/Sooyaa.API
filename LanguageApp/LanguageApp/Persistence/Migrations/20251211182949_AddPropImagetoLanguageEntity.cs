using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPropImagetoLanguageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Languages");
        }
    }
}
