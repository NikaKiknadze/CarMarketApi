using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketApi.Migrations
{
    /// <inheritdoc />
    public partial class AnotherChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyersPersonalInformations_Buyers_UserId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                newName: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyersPersonalInformations_BuyerId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "BuyerId",
                unique: true,
                filter: "[BuyerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyersPersonalInformations_Buyers_BuyerId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "BuyerId",
                principalSchema: "market",
                principalTable: "Buyers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyersPersonalInformations_Buyers_BuyerId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_BuyersPersonalInformations_BuyerId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                schema: "market",
                table: "BuyersPersonalInformations",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyersPersonalInformations_Buyers_UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "UserId",
                principalSchema: "market",
                principalTable: "Buyers",
                principalColumn: "Id");
        }
    }
}
