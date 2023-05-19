using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RouteService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfSeatsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Routes");
        }
    }
}
