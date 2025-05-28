using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class Added_DisputeMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisputeMessages",
                columns: table => new
                {
                    DepositDisputeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepositWithheld = table.Column<decimal>(type: "TEXT", nullable: false),
                    WithheldReason = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputeMessages", x => x.DepositDisputeId);
                    table.ForeignKey(
                        name: "FK_DisputeMessages_DepositeDisputes_DepositDisputeId",
                        column: x => x.DepositDisputeId,
                        principalTable: "DepositeDisputes",
                        principalColumn: "DepositDisputeId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisputeMessages");
        }
    }
}
