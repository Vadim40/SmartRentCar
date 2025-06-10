using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class AddFeilds_Rental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisputeRequested",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEarlyEndConfirmedByCompany",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEarlyEndConfirmedByRenter",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEndConfirmedByCompany",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEndConfirmedByRenter",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStartConfirmedByCompany",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStartConfirmedByRenter",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisputeRequested",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsEarlyEndConfirmedByCompany",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsEarlyEndConfirmedByRenter",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsEndConfirmedByCompany",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsEndConfirmedByRenter",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsStartConfirmedByCompany",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsStartConfirmedByRenter",
                table: "Rentals");
        }
    }
}
