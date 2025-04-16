using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartRentCar.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRentContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pass",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ContractAddress",
                table: "RentContracts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractAddress",
                table: "RentContracts");

            migrationBuilder.AddColumn<string>(
                name: "Pass",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
