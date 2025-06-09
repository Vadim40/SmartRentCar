using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Rental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContractAddress",
                table: "Rentals",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepositDisputeId",
                table: "Rentals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DepositDisputeId",
                table: "Rentals",
                column: "DepositDisputeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_DepositeDisputes_DepositDisputeId",
                table: "Rentals",
                column: "DepositDisputeId",
                principalTable: "DepositeDisputes",
                principalColumn: "DepositDisputeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_DepositeDisputes_DepositDisputeId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DepositDisputeId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ContractAddress",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DepositDisputeId",
                table: "Rentals");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepositDisputeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractAddress = table.Column<string>(type: "TEXT", nullable: false),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_DepositeDisputes_DepositDisputeId",
                        column: x => x.DepositDisputeId,
                        principalTable: "DepositeDisputes",
                        principalColumn: "DepositDisputeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DepositDisputeId",
                table: "Contracts",
                column: "DepositDisputeId");
        }
    }
}
