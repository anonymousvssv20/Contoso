using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class migrate_something_and_something_more : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentVMID",
                table: "Enrollment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentVMID",
                table: "Enrollment",
                column: "StudentVMID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_StudentVM_StudentVMID",
                table: "Enrollment",
                column: "StudentVMID",
                principalTable: "StudentVM",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_StudentVM_StudentVMID",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentVMID",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "StudentVMID",
                table: "Enrollment");
        }
    }
}
