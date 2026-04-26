using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addStockItemsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreStock");

            migrationBuilder.CreateTable(
                name: "StoreStockLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ItemId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    StoreId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStockLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreStockLevels_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStockLevels_Items_ItemId1",
                        column: x => x.ItemId1,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreStockLevels_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStockLevels_Stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockLevels_ItemId",
                table: "StoreStockLevels",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockLevels_ItemId1",
                table: "StoreStockLevels",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockLevels_StoreId",
                table: "StoreStockLevels",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockLevels_StoreId1",
                table: "StoreStockLevels",
                column: "StoreId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreStockLevels");

            migrationBuilder.CreateTable(
                name: "StoreStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    StoreId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreStock_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStock_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStock_Stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreStock_ItemId",
                table: "StoreStock",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStock_StoreId",
                table: "StoreStock",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStock_StoreId1",
                table: "StoreStock",
                column: "StoreId1");
        }
    }
}
