using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class _4th : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffSalaryId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "staffSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SalaryAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staffSalaries_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StaffSalaryId",
                table: "Teachers",
                column: "StaffSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_staffSalaries_TeacherId",
                table: "staffSalaries",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_staffSalaries_StaffSalaryId",
                table: "Teachers",
                column: "StaffSalaryId",
                principalTable: "staffSalaries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_staffSalaries_StaffSalaryId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "staffSalaries");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StaffSalaryId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StaffSalaryId",
                table: "Teachers");
        }
    }
}
