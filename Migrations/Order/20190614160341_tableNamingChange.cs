using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.Order
{
    public partial class tableNamingChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItemDimensionUnitDAL");

            migrationBuilder.DropTable(
                name: "OrdersAddressesDAL");

            migrationBuilder.DropTable(
                name: "OrdersDeliveryItemsDAL");

            migrationBuilder.CreateTable(
                name: "DeliveryItemDimensionUnit",
                columns: table => new
                {
                    DeliveryItemId = table.Column<Guid>(nullable: false),
                    DimensionalItemId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UnitAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItemDimensionUnit", x => new { x.DeliveryItemId, x.DimensionalItemId });
                    table.ForeignKey(
                        name: "FK_DeliveryItemDimensionUnit_DeliveryItem_DeliveryItemId",
                        column: x => x.DeliveryItemId,
                        principalTable: "DeliveryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryItemDimensionUnit_DimensionalUnit_DimensionalItemId",
                        column: x => x.DimensionalItemId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersAddresses",
                columns: table => new
                {
                    AddressFromId = table.Column<Guid>(nullable: false),
                    AddressToId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersAddresses", x => new { x.AddressFromId, x.AddressToId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrdersAddresses_Address_AddressFromId",
                        column: x => x.AddressFromId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersAddresses_Address_AddressToId",
                        column: x => x.AddressToId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersAddresses_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDeliveryItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    DeliveryId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDeliveryItems", x => new { x.OrderId, x.DeliveryId });
                    table.ForeignKey(
                        name: "FK_OrdersDeliveryItems_DeliveryItem_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "DeliveryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersDeliveryItems_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DeliveryItemDimensionUnit",
                columns: new[] { "DeliveryItemId", "DimensionalItemId", "Id", "UnitAmount" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000000"), 0f });

            migrationBuilder.InsertData(
                table: "OrdersAddresses",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000000"), new Guid("60000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "OrdersAddresses",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemDimensionUnit_DimensionalItemId",
                table: "DeliveryItemDimensionUnit",
                column: "DimensionalItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddresses_AddressToId",
                table: "OrdersAddresses",
                column: "AddressToId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddresses_OrderId",
                table: "OrdersAddresses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDeliveryItems_DeliveryId",
                table: "OrdersDeliveryItems",
                column: "DeliveryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItemDimensionUnit");

            migrationBuilder.DropTable(
                name: "OrdersAddresses");

            migrationBuilder.DropTable(
                name: "OrdersDeliveryItems");

            migrationBuilder.CreateTable(
                name: "DeliveryItemDimensionUnitDAL",
                columns: table => new
                {
                    DeliveryItemId = table.Column<Guid>(nullable: false),
                    DimensionalItemId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UnitAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItemDimensionUnitDAL", x => new { x.DeliveryItemId, x.DimensionalItemId });
                    table.ForeignKey(
                        name: "FK_DeliveryItemDimensionUnitDAL_DeliveryItem_DeliveryItemId",
                        column: x => x.DeliveryItemId,
                        principalTable: "DeliveryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryItemDimensionUnitDAL_DimensionalUnit_DimensionalItemId",
                        column: x => x.DimensionalItemId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersAddressesDAL",
                columns: table => new
                {
                    AddressFromId = table.Column<Guid>(nullable: false),
                    AddressToId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersAddressesDAL", x => new { x.AddressFromId, x.AddressToId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrdersAddressesDAL_Address_AddressFromId",
                        column: x => x.AddressFromId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersAddressesDAL_Address_AddressToId",
                        column: x => x.AddressToId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersAddressesDAL_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDeliveryItemsDAL",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    DeliveryId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDeliveryItemsDAL", x => new { x.OrderId, x.DeliveryId });
                    table.ForeignKey(
                        name: "FK_OrdersDeliveryItemsDAL_DeliveryItem_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "DeliveryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersDeliveryItemsDAL_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DeliveryItemDimensionUnitDAL",
                columns: new[] { "DeliveryItemId", "DimensionalItemId", "Id", "UnitAmount" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000000"), 0f });

            migrationBuilder.InsertData(
                table: "OrdersAddressesDAL",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000000"), new Guid("60000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "OrdersAddressesDAL",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemDimensionUnitDAL_DimensionalItemId",
                table: "DeliveryItemDimensionUnitDAL",
                column: "DimensionalItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddressesDAL_AddressToId",
                table: "OrdersAddressesDAL",
                column: "AddressToId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddressesDAL_OrderId",
                table: "OrdersAddressesDAL",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDeliveryItemsDAL_DeliveryId",
                table: "OrdersDeliveryItemsDAL",
                column: "DeliveryId");
        }
    }
}
