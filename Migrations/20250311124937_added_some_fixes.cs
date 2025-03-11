using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class added_some_fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Instructor_InstructorID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Student_StudentID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_StudentID",
                table: "Users",
                newName: "IX_Users_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_User_InstructorID",
                table: "Users",
                newName: "IX_Users_InstructorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Instructor_InstructorID",
                table: "Users",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Student_StudentID",
                table: "Users",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Instructor_InstructorID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Student_StudentID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_StudentID",
                table: "User",
                newName: "IX_User_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_InstructorID",
                table: "User",
                newName: "IX_User_InstructorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Instructor_InstructorID",
                table: "User",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Student_StudentID",
                table: "User",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
