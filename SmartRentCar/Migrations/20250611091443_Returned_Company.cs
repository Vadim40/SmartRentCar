using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartRentCar.Migrations
{
    /// <inheritdoc />
    public partial class Returned_Company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Companys_CompanyId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "TestCarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companys",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Companys");

            migrationBuilder.RenameTable(
                name: "Companys",
                newName: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "ContractAddress",
                table: "RentContracts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "RentContracts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Cars",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Company",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RentContracts_CompanyId",
                table: "RentContracts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Company_CompanyId",
                table: "Cars",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentContracts_Company_CompanyId",
                table: "RentContracts",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Company_CompanyId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_RentContracts_Company_CompanyId",
                table: "RentContracts");

            migrationBuilder.DropIndex(
                name: "IX_RentContracts_CompanyId",
                table: "RentContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "RentContracts");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companys");

            migrationBuilder.AlterColumn<string>(
                name: "ContractAddress",
                table: "RentContracts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Companys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companys",
                table: "Companys",
                column: "CompanyId");

            migrationBuilder.CreateTable(
                name: "TestCarImages",
                columns: table => new
                {
                    CarImageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageData = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCarImages", x => x.CarImageId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Companys_CompanyId",
                table: "Cars",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
