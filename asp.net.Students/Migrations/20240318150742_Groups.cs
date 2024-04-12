using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.net.Students.Migrations
{
    /// <inheritdoc />
    public partial class Groups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoSubjects");

            migrationBuilder.CreateTable(
                name: "InfoSubject",
                columns: table => new
                {
                    StudentsListId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSubject", x => new { x.StudentsListId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_InfoSubject_Infos_StudentsListId",
                        column: x => x.StudentsListId,
                        principalTable: "Infos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoSubject_SubjectsId",
                table: "InfoSubject",
                column: "SubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoSubject");

            migrationBuilder.CreateTable(
                name: "InfoSubjects",
                columns: table => new
                {
                    StudentsListId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSubjects", x => new { x.StudentsListId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_InfoSubjects_Infos_StudentsListId",
                        column: x => x.StudentsListId,
                        principalTable: "Infos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoSubjects_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoSubjects_SubjectsId",
                table: "InfoSubjects",
                column: "SubjectsId");
        }
    }
}
