using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketApi.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyers_BuyersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Buyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_SellersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_SellersPersonalInformations_SellerId",
                schema: "market",
                table: "SellersPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_PersonalInformationId",
                schema: "market",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_Buyers_PersonalInformationId",
                schema: "market",
                table: "Buyers");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                schema: "market",
                table: "Buyers",
                newName: "PersonalIformationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellersPersonalInformations_SellerId",
                schema: "market",
                table: "SellersPersonalInformations",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SellersPersonalInformations_SellerId",
                schema: "market",
                table: "SellersPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations");

            migrationBuilder.RenameColumn(
                name: "PersonalIformationId",
                schema: "market",
                table: "Buyers",
                newName: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellersPersonalInformations_SellerId",
                schema: "market",
                table: "SellersPersonalInformations",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_PersonalInformationId",
                schema: "market",
                table: "Sellers",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_PersonalInformationId",
                schema: "market",
                table: "Buyers",
                column: "PersonalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyers_BuyersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Buyers",
                column: "PersonalInformationId",
                principalSchema: "market",
                principalTable: "BuyersPersonalInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_SellersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Sellers",
                column: "PersonalInformationId",
                principalSchema: "market",
                principalTable: "SellersPersonalInformations",
                principalColumn: "Id");
        }
    }
}
