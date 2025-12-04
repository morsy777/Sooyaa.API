using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForLessonPerChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_Title",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Title_ChapterId",
                table: "Lessons",
                columns: new[] { "Title", "ChapterId" },
                unique: true,
                filter: "[ChapterId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_Title_ChapterId",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Title",
                table: "Lessons",
                column: "Title",
                unique: true);
        }
    }
}
