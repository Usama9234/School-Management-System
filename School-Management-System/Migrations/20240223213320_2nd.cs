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
                name: "FeesFeeId",
                table: "Class",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Class_FeesFeeId",
                table: "Class",
                column: "FeesFeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_fees_FeesFeeId",
                table: "Class",
                column: "FeesFeeId",
                principalTable: "fees",
                principalColumn: "FeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_fees_FeesFeeId",
                table: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Class_FeesFeeId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "FeesFeeId",
                table: "Class");
        }
    }
}
