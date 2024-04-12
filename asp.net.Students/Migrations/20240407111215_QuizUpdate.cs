using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.net.Students.Migrations
{
    /// <inheritdoc />
    public partial class QuizUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "QuizStudentResults",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Groups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f2447f0-28fa-44cd-9d8d-ffd3142827bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "49d8d26f-f1d1-4c25-8f81-502f9f781b5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e194222b-96f1-4304-b58f-3ca8516de981");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "dceb6d8d-a76b-429c-a708-210aedc2f554");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_QuizId",
                table: "Groups",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Quizzes_QuizId",
                table: "Groups",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Quizzes_QuizId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_QuizId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "QuizStudentResults");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "85e6e66f-2c1d-47d0-9132-d9ac2205372e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c8115001-0311-429b-9571-f2f8ecc57520");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "844268f6-7982-47ce-9dd0-2886a4e2c724");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "c45ba750-ff3f-4daa-921d-8b294bd2eaf3");
        }
    }
}
