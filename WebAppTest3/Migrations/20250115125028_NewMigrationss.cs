using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTest3.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepatmentId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepatmentId",
                table: "Employees",
                type: "int",
                nullable: true);
        }
    }
}
