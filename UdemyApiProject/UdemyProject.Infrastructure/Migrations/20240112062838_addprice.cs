using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");
        }
    }
}
