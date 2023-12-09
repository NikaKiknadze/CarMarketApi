using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketApi.Migrations
{
    /// <inheritdoc />
    public partial class DBTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "market");

            migrationBuilder.CreateTable(
                name: "Buyers",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyersPersonalInformations",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyersPersonalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyersPersonalInformations_Buyers_UserId",
                        column: x => x.UserId,
                        principalSchema: "market",
                        principalTable: "Buyers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
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
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "market",
                        principalTable: "Buyers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellersPersonalInformations",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellersPersonalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellersPersonalInformations_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SellersUsersJoin",
                schema: "market",
                columns: table => new
                {
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellersUsersJoin", x => new { x.BuyerId, x.SellerId });
                    table.ForeignKey(
                        name: "FK_SellersUsersJoin_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "market",
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellersUsersJoin_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_PersonalInformationId",
                schema: "market",
                table: "Buyers",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyersPersonalInformations_UserId",
                schema: "market",
                table: "BuyersPersonalInformations",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_PersonalInformationId",
                schema: "market",
                table: "Sellers",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellersPersonalInformations_SellerId",
                schema: "market",
                table: "SellersPersonalInformations",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_SellersUsersJoin_SellerId",
                schema: "market",
                table: "SellersUsersJoin",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyers_BuyersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Buyers",
                column: "PersonalInformationId",
                principalSchema: "market",
                principalTable: "BuyersPersonalInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Sellers_SellerId",
                schema: "market",
                table: "Cars",
                column: "SellerId",
                principalSchema: "market",
                principalTable: "Sellers",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyers_BuyersPersonalInformations_PersonalInformationId",
                schema: "market",
                table: "Buyers");

            migrationBuilder.DropForeignKey(
                name: "FK_SellersPersonalInformations_Sellers_SellerId",
                schema: "market",
                table: "SellersPersonalInformations");

            migrationBuilder.DropTable(
                name: "Cars",
                schema: "market");

            migrationBuilder.DropTable(
                name: "SellersUsersJoin",
                schema: "market");

            migrationBuilder.DropTable(
                name: "BuyersPersonalInformations",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Buyers",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "market");

            migrationBuilder.DropTable(
                name: "SellersPersonalInformations",
                schema: "market");
        }
    }
}
