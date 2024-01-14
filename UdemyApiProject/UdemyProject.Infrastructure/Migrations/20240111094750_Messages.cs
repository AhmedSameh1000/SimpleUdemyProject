using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CongratulationsMessage",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WelcomeMessage",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CongratulationsMessage",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WelcomeMessage",
                table: "Courses");
        }
    }
}
