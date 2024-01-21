using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_hours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "totalHours",
                table: "Courses",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalHours",
                table: "Courses");
        }
    }
}
