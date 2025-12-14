using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddsavedLessonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedLessons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedLessons_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedLessons_LanguageId",
                table: "SavedLessons",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedLessons_LessonId",
                table: "SavedLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedLessons_UserId_LessonId_LanguageId",
                table: "SavedLessons",
                columns: new[] { "UserId", "LessonId", "LanguageId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedLessons");
        }
    }
}
