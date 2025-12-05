using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForLevelPerLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Levels_Name",
                table: "Levels");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels",
                columns: new[] { "Name", "LanguageId" },
                unique: true,
                filter: "[LanguageId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name",
                table: "Levels",
                column: "Name",
                unique: true);
        }
    }
}
