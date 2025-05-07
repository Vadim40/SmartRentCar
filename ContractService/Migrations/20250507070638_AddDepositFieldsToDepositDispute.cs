using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class AddDepositFieldsToDepositDispute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "DepositeDisputes",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DepositWithheld",
                table: "DepositeDisputes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "DepositeDisputes");

            migrationBuilder.DropColumn(
                name: "DepositWithheld",
                table: "DepositeDisputes");
        }
    }
}
