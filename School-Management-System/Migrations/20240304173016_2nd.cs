using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class _2nd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedSubjectsAssignedSubjectId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedSubjectsAssignedSubjectId",
                table: "Class",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AssignedSubjectsAssignedSubjectId",
                table: "Teachers",
                column: "AssignedSubjectsAssignedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_AssignedSubjectsAssignedSubjectId",
                table: "Class",
                column: "AssignedSubjectsAssignedSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_assignedSubjects_AssignedSubjectsAssignedSubjectId",
                table: "Class",
                column: "AssignedSubjectsAssignedSubjectId",
                principalTable: "assignedSubjects",
                principalColumn: "AssignedSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_assignedSubjects_AssignedSubjectsAssignedSubjectId",
                table: "Teachers",
                column: "AssignedSubjectsAssignedSubjectId",
                principalTable: "assignedSubjects",
                principalColumn: "AssignedSubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_assignedSubjects_AssignedSubjectsAssignedSubjectId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_assignedSubjects_AssignedSubjectsAssignedSubjectId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_AssignedSubjectsAssignedSubjectId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Class_AssignedSubjectsAssignedSubjectId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "AssignedSubjectsAssignedSubjectId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AssignedSubjectsAssignedSubjectId",
                table: "Class");
        }
    }
}
