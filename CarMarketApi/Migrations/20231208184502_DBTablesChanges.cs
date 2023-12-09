using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketApi.Migrations
{
    /// <inheritdoc />
    public partial class DBTablesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars",
                schema: "market");

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cost = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true),
                    BuyerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "market",
                        principalTable: "Buyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BuyerId",
                schema: "market",
                table: "Items",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SellerId",
                schema: "market",
                table: "Items",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "market");

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "market",
                        principalTable: "Buyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BuyerId",
                schema: "market",
                table: "Cars",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SellerId",
                schema: "market",
                table: "Cars",
                column: "SellerId");
        }
    }
}
