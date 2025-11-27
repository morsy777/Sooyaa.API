using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAndUserAttemptsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAttempts_Questions_QuestionId1",
                table: "UserAttempts");

            migrationBuilder.DropIndex(
                name: "IX_UserAttempts_QuestionId1",
                table: "UserAttempts");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "UserAttempts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "UserAttempts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAttempts_QuestionId1",
                table: "UserAttempts",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttempts_Questions_QuestionId1",
                table: "UserAttempts",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
