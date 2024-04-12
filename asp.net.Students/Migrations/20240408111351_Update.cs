using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.net.Students.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizStudentAnswers_Infos_StudentId",
                table: "QuizStudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizStudentResults_Infos_StudentId",
                table: "QuizStudentResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Subjects_SubjectId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Quizzes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "493ebaa2-0372-4055-89af-d89d3e424b19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e064c826-47f3-4592-85ca-e9dbc3764502");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5636c448-ed8d-4bb3-ab24-69661abdca2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a5f71815-ef94-4e56-9ef0-fbcfeb7c320b");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizStudentAnswers_AspNetUsers_StudentId",
                table: "QuizStudentAnswers",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizStudentResults_AspNetUsers_StudentId",
                table: "QuizStudentResults",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Subjects_SubjectId",
                table: "Quizzes",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizStudentAnswers_AspNetUsers_StudentId",
                table: "QuizStudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizStudentResults_AspNetUsers_StudentId",
                table: "QuizStudentResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Subjects_SubjectId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Quizzes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ff01b74a-3667-45e7-a30a-47df75607211");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "25e35345-33d3-4e25-8f8b-a4375ebd027e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6cd0e098-7f32-41de-8b72-10330e2f85a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "5880f414-a68b-4676-ac54-d331b28793ba");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizStudentAnswers_Infos_StudentId",
                table: "QuizStudentAnswers",
                column: "StudentId",
                principalTable: "Infos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizStudentResults_Infos_StudentId",
                table: "QuizStudentResults",
                column: "StudentId",
                principalTable: "Infos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Subjects_SubjectId",
                table: "Quizzes",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
