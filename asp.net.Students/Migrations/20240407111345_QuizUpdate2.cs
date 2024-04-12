using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.net.Students.Migrations
{
    /// <inheritdoc />
    public partial class QuizUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Quizzes_QuizId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_QuizId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "GroupQuiz",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuizzesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupQuiz", x => new { x.GroupsId, x.QuizzesId });
                    table.ForeignKey(
                        name: "FK_GroupQuiz_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupQuiz_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_GroupQuiz_QuizzesId",
                table: "GroupQuiz",
                column: "QuizzesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupQuiz");

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
    }
}
