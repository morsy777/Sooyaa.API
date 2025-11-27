using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesToAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Level_SelectedLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Level_LevelId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Category_CategoryId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Chapter_ChapterId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Level_Language_LanguageId",
                table: "Level");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Lesson_LessonId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAttempt_AspNetUsers_UserId",
                table: "UserAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAttempt_Question_QuestionId",
                table: "UserAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguage_AspNetUsers_UserId",
                table: "UserLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguage_Language_LanguageId",
                table: "UserLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgress_AspNetUsers_UserId",
                table: "UserProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgress_Lesson_LessonId",
                table: "UserProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStreak_AspNetUsers_UserId",
                table: "UserStreak");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStreak",
                table: "UserStreak");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProgress",
                table: "UserProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLanguage",
                table: "UserLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAttempt",
                table: "UserAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "UserStreak",
                newName: "UserStreaks");

            migrationBuilder.RenameTable(
                name: "UserProgress",
                newName: "UserProgresses");

            migrationBuilder.RenameTable(
                name: "UserLanguage",
                newName: "UserLanguages");

            migrationBuilder.RenameTable(
                name: "UserAttempt",
                newName: "UserAttempts");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "Levels");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Lessons");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Chapter",
                newName: "Chapters");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_UserStreak_UserId",
                table: "UserStreaks",
                newName: "IX_UserStreaks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgress_UserId",
                table: "UserProgresses",
                newName: "IX_UserProgresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgress_LessonId",
                table: "UserProgresses",
                newName: "IX_UserProgresses_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguage_UserId",
                table: "UserLanguages",
                newName: "IX_UserLanguages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguage_LanguageId",
                table: "UserLanguages",
                newName: "IX_UserLanguages_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAttempt_UserId",
                table: "UserAttempts",
                newName: "IX_UserAttempts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAttempt_QuestionId",
                table: "UserAttempts",
                newName: "IX_UserAttempts_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_LessonId",
                table: "Questions",
                newName: "IX_Questions_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Level_LanguageId",
                table: "Levels",
                newName: "IX_Levels_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_ChapterId",
                table: "Lessons",
                newName: "IX_Lessons_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_CategoryId",
                table: "Lessons",
                newName: "IX_Lessons_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_LevelId",
                table: "Chapters",
                newName: "IX_Chapters_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAttempts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAttempts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Levels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Lessons",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStreaks",
                table: "UserStreaks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProgresses",
                table: "UserProgresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLanguages",
                table: "UserLanguages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAttempts",
                table: "UserAttempts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_SelectedLevelId",
                table: "AspNetUsers",
                column: "SelectedLevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Levels_LevelId",
                table: "Chapters",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Chapters_ChapterId",
                table: "Lessons",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttempts_AspNetUsers_UserId",
                table: "UserAttempts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttempts_Questions_QuestionId",
                table: "UserAttempts",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_AspNetUsers_UserId",
                table: "UserLanguages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId",
                table: "UserLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_AspNetUsers_UserId",
                table: "UserProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_Lessons_LessonId",
                table: "UserProgresses",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStreaks_AspNetUsers_UserId",
                table: "UserStreaks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_SelectedLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Levels_LevelId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Chapters_ChapterId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Languages_LanguageId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAttempts_AspNetUsers_UserId",
                table: "UserAttempts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAttempts_Questions_QuestionId",
                table: "UserAttempts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_AspNetUsers_UserId",
                table: "UserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId",
                table: "UserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_AspNetUsers_UserId",
                table: "UserProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_Lessons_LessonId",
                table: "UserProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStreaks_AspNetUsers_UserId",
                table: "UserStreaks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStreaks",
                table: "UserStreaks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProgresses",
                table: "UserProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLanguages",
                table: "UserLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAttempts",
                table: "UserAttempts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "UserStreaks",
                newName: "UserStreak");

            migrationBuilder.RenameTable(
                name: "UserProgresses",
                newName: "UserProgress");

            migrationBuilder.RenameTable(
                name: "UserLanguages",
                newName: "UserLanguage");

            migrationBuilder.RenameTable(
                name: "UserAttempts",
                newName: "UserAttempt");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Level");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lesson");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Chapters",
                newName: "Chapter");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_UserStreaks_UserId",
                table: "UserStreak",
                newName: "IX_UserStreak_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgresses_UserId",
                table: "UserProgress",
                newName: "IX_UserProgress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgresses_LessonId",
                table: "UserProgress",
                newName: "IX_UserProgress_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguage",
                newName: "IX_UserLanguage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguages_LanguageId",
                table: "UserLanguage",
                newName: "IX_UserLanguage_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAttempts_UserId",
                table: "UserAttempt",
                newName: "IX_UserAttempt_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAttempts_QuestionId",
                table: "UserAttempt",
                newName: "IX_UserAttempt_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_LessonId",
                table: "Question",
                newName: "IX_Question_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_LanguageId",
                table: "Level",
                newName: "IX_Level_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_ChapterId",
                table: "Lesson",
                newName: "IX_Lesson_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CategoryId",
                table: "Lesson",
                newName: "IX_Lesson_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_LevelId",
                table: "Chapter",
                newName: "IX_Chapter_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAttempt",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAttempt",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Level",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Lesson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Lesson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStreak",
                table: "UserStreak",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProgress",
                table: "UserProgress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLanguage",
                table: "UserLanguage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAttempt",
                table: "UserAttempt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_SelectedLevelId",
                table: "AspNetUsers",
                column: "SelectedLevelId",
                principalTable: "Level",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Level_LevelId",
                table: "Chapter",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Category_CategoryId",
                table: "Lesson",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Chapter_ChapterId",
                table: "Lesson",
                column: "ChapterId",
                principalTable: "Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Level_Language_LanguageId",
                table: "Level",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Lesson_LessonId",
                table: "Question",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttempt_AspNetUsers_UserId",
                table: "UserAttempt",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttempt_Question_QuestionId",
                table: "UserAttempt",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguage_AspNetUsers_UserId",
                table: "UserLanguage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguage_Language_LanguageId",
                table: "UserLanguage",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgress_AspNetUsers_UserId",
                table: "UserProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgress_Lesson_LessonId",
                table: "UserProgress",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStreak_AspNetUsers_UserId",
                table: "UserStreak",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
