using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class _5th : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjetsSubjectId",
                table: "Class",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "subjets",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjets", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_subjets_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_SubjetsSubjectId",
                table: "Class",
                column: "SubjetsSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_subjets_ClassId",
                table: "subjets",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_subjets_SubjetsSubjectId",
                table: "Class",
                column: "SubjetsSubjectId",
                principalTable: "subjets",
                principalColumn: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_subjets_SubjetsSubjectId",
                table: "Class");

            migrationBuilder.DropTable(
                name: "subjets");

            migrationBuilder.DropIndex(
                name: "IX_Class_SubjetsSubjectId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "SubjetsSubjectId",
                table: "Class");
        }
    }
}
