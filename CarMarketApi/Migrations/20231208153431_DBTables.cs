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
                name: "Sellers",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "market",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateInformations",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalId = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    MobilePhone = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateInformations_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrivateInformations_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "market",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SellersUsersJoin",
                schema: "market",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellersUsersJoin", x => new { x.UserId, x.SellerId });
                    table.ForeignKey(
                        name: "FK_SellersUsersJoin_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "market",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellersUsersJoin_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "market",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SellerId",
                schema: "market",
                table: "Cars",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                schema: "market",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateInformations_SellerId",
                schema: "market",
                table: "PrivateInformations",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateInformations_UserId",
                schema: "market",
                table: "PrivateInformations",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SellersUsersJoin_SellerId",
                schema: "market",
                table: "SellersUsersJoin",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars",
                schema: "market");

            migrationBuilder.DropTable(
                name: "PrivateInformations",
                schema: "market");

            migrationBuilder.DropTable(
                name: "SellersUsersJoin",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "market");
        }
    }
}
