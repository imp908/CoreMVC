using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.Order
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimensionalUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionalUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ItemsOrderedAmount = table.Column<int>(nullable: false),
                    DeliveryPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItemDimensionUnitDAL",
                columns: table => new
                {
                    DeliveryItemId = table.Column<Guid>(nullable: false),
                    DimensionalItemId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<float>(nullable: false)
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
                    // table.ForeignKey(
                    //     name: "FK_DeliveryItemDimensionUnitDAL_DimensionalUnit_Id",
                    //     column: x => x.Id,
                    //     principalTable: "DimensionalUnit",
                    //     principalColumn: "Id",
                    //     onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitsConvertion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromId = table.Column<Guid>(nullable: false),
                    ToId = table.Column<Guid>(nullable: false),
                    ConvertionRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsConvertion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitsConvertion_DimensionalUnit_FromId",
                        column: x => x.FromId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitsConvertion_DimensionalUnit_ToId",
                        column: x => x.ToId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersAdresses",
                columns: table => new
                {
                    AddressFromId = table.Column<Guid>(nullable: false),
                    AddressToId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersAdresses", x => new { x.AddressFromId, x.AddressToId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrdersAdresses_Address_AddressFromId",
                        column: x => x.AddressFromId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersAdresses_Address_AddressToId",
                        column: x => x.AddressToId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersAdresses_Order_OrderId",
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
                table: "Address",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000000"), "Some address one" },
                    { new Guid("30000000-0000-0000-0000-000000000001"), "Some address two" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryItem",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000000"), "Item1" },
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Item2" }
                });

            migrationBuilder.InsertData(
                table: "DimensionalUnit",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "wight in kg", "kg" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "wight in pounds", "lbs" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "lenght in sm", "sm" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "lenght in inches", "inch" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "DeliveryPrice", "ItemsOrderedAmount", "Name" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000000"), 0f, 0, "Order one" },
                    { new Guid("50000000-0000-0000-0000-000000000001"), 0f, 0, "Order two" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryItemDimensionUnitDAL",
                columns: new[] { "DeliveryItemId", "DimensionalItemId", "Amount", "Id" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000001"), 0f, new Guid("40000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "OrdersAdresses",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000000"), new Guid("60000000-0000-0000-0000-000000000000") },
                    { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "UnitsConvertion",
                columns: new[] { "Id", "ConvertionRate", "FromId", "ToId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000000"), 2.20462f, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("10000000-0000-0000-0000-000000000001"), 0.220462f, new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemDimensionUnitDAL_Id",
                table: "DeliveryItemDimensionUnitDAL",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAdresses_AddressToId",
                table: "OrdersAdresses",
                column: "AddressToId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAdresses_OrderId",
                table: "OrdersAdresses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDeliveryItemsDAL_DeliveryId",
                table: "OrdersDeliveryItemsDAL",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsConvertion_FromId",
                table: "UnitsConvertion",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsConvertion_ToId",
                table: "UnitsConvertion",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItemDimensionUnitDAL");

            migrationBuilder.DropTable(
                name: "OrdersAdresses");

            migrationBuilder.DropTable(
                name: "OrdersDeliveryItemsDAL");

            migrationBuilder.DropTable(
                name: "UnitsConvertion");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DeliveryItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "DimensionalUnit");
        }
    }
}
