using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreasuryManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCollateralColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Collaterals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Collaterals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Collaterals");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Collaterals");
        }
    }
}
