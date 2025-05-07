using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositeDisputes_Contracts_ContractId",
                table: "DepositeDisputes");

            migrationBuilder.DropIndex(
                name: "IX_DepositeDisputes_ContractId",
                table: "DepositeDisputes");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "DepositeDisputes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "DepositeDisputes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DepositeDisputes_ContractId",
                table: "DepositeDisputes",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositeDisputes_Contracts_ContractId",
                table: "DepositeDisputes",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
