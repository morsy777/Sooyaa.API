using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenCategoriesandLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LanguageId",
                table: "Categories",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Languages_LanguageId",
                table: "Categories",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Languages_LanguageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LanguageId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Categories");
        }
    }
}
