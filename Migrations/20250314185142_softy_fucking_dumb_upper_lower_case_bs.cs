using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class softy_fucking_dumb_upper_lower_case_bs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Post",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Post",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Post",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Post",
                newName: "content");
        }
    }
}
