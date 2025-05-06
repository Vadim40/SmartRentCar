using System;
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
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "DisputeStatuses",
                columns: table => new
                {
                    DisputeStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputeStatuses", x => x.DisputeStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RentalStatuses",
                columns: table => new
                {
                    RentalStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalStatuses", x => x.RentalStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Deposit = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalStatuses_RentalStatusId",
                        column: x => x.RentalStatusId,
                        principalTable: "RentalStatuses",
                        principalColumn: "RentalStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepositDisputeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                });

            migrationBuilder.CreateTable(
                name: "DepositeDisputes",
                columns: table => new
                {
                    DepositDisputeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    DisputeStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositeDisputes", x => x.DepositDisputeId);
                    table.ForeignKey(
                        name: "FK_DepositeDisputes_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositeDisputes_DisputeStatuses_DisputeStatusId",
                        column: x => x.DisputeStatusId,
                        principalTable: "DisputeStatuses",
                        principalColumn: "DisputeStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalDocuments",
                columns: table => new
                {
                    RentalDocumentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepositDisputeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDocuments", x => x.RentalDocumentId);
                    table.ForeignKey(
                        name: "FK_RentalDocuments_DepositeDisputes_DepositDisputeId",
                        column: x => x.DepositDisputeId,
                        principalTable: "DepositeDisputes",
                        principalColumn: "DepositDisputeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalDocuments_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "RentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DepositDisputeId",
                table: "Contracts",
                column: "DepositDisputeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositeDisputes_ContractId",
                table: "DepositeDisputes",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositeDisputes_DisputeStatusId",
                table: "DepositeDisputes",
                column: "DisputeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDocuments_DepositDisputeId",
                table: "RentalDocuments",
                column: "DepositDisputeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDocuments_RentalId",
                table: "RentalDocuments",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalStatusId",
                table: "Rentals",
                column: "RentalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_DepositeDisputes_DepositDisputeId",
                table: "Contracts",
                column: "DepositDisputeId",
                principalTable: "DepositeDisputes",
                principalColumn: "DepositDisputeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_DepositeDisputes_DepositDisputeId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "RentalDocuments");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "RentalStatuses");

            migrationBuilder.DropTable(
                name: "DepositeDisputes");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "DisputeStatuses");
        }
    }
}
