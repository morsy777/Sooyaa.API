using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForChapterPerLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_Name",
                table: "Chapters");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels",
                columns: new[] { "Name", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_Name_LevelId",
                table: "Chapters",
                columns: new[] { "Name", "LevelId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_Name_LevelId",
                table: "Chapters");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Levels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Chapters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_LanguageId",
                table: "Levels",
                columns: new[] { "Name", "LanguageId" },
                unique: true,
                filter: "[LanguageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_Name",
                table: "Chapters",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
