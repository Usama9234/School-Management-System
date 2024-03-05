using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class _3rd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "assignedSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "assignedSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Teachername",
                table: "assignedSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "assignedSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "assignedSubjects");

            migrationBuilder.DropColumn(
                name: "Teachername",
                table: "assignedSubjects");
        }
    }
}
