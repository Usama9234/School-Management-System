using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class _1st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeacherDOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TeacherEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeacherAdress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherDetailsTeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_Teachers_TeacherDetailsTeacherId",
                        column: x => x.TeacherDetailsTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId");
                });

            migrationBuilder.CreateTable(
                name: "assignedSubjects",
                columns: table => new
                {
                    AssignedSubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedSubjects", x => x.AssignedSubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FeesFeeId = table.Column<int>(type: "int", nullable: true),
                    StudentDetailsStudentId = table.Column<int>(type: "int", nullable: true),
                    SubjectsSubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "fees",
                columns: table => new
                {
                    FeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    FeeAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fees", x => x.FeeId);
                    table.ForeignKey(
                        name: "FK_fees_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentDOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentAdress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentRollNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_students_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjets",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedSubjectsAssignedSubjectId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_subjets_assignedSubjects_AssignedSubjectsAssignedSubjectId",
                        column: x => x.AssignedSubjectsAssignedSubjectId,
                        principalTable: "assignedSubjects",
                        principalColumn: "AssignedSubjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignedSubjects_SubjectId",
                table: "assignedSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_FeesFeeId",
                table: "Class",
                column: "FeesFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_StudentDetailsStudentId",
                table: "Class",
                column: "StudentDetailsStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SubjectsSubjectId",
                table: "Class",
                column: "SubjectsSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_fees_ClassId",
                table: "fees",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassId",
                table: "students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_subjets_AssignedSubjectsAssignedSubjectId",
                table: "subjets",
                column: "AssignedSubjectsAssignedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_subjets_ClassId",
                table: "subjets",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TeacherDetailsTeacherId",
                table: "Teachers",
                column: "TeacherDetailsTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedSubjects_subjets_SubjectId",
                table: "assignedSubjects",
                column: "SubjectId",
                principalTable: "subjets",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Class_fees_FeesFeeId",
                table: "Class",
                column: "FeesFeeId",
                principalTable: "fees",
                principalColumn: "FeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_students_StudentDetailsStudentId",
                table: "Class",
                column: "StudentDetailsStudentId",
                principalTable: "students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_subjets_SubjectsSubjectId",
                table: "Class",
                column: "SubjectsSubjectId",
                principalTable: "subjets",
                principalColumn: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedSubjects_subjets_SubjectId",
                table: "assignedSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_subjets_SubjectsSubjectId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_fees_FeesFeeId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_students_StudentDetailsStudentId",
                table: "Class");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "subjets");

            migrationBuilder.DropTable(
                name: "assignedSubjects");

            migrationBuilder.DropTable(
                name: "fees");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "Class");
        }
    }
}
