using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.Order
{
    public partial class AddressEntityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersAdresses");

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

            migrationBuilder.InsertData(
                table: "OrdersAddressesDAL",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000000"), new Guid("60000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "OrdersAddressesDAL",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddressesDAL_AddressToId",
                table: "OrdersAddressesDAL",
                column: "AddressToId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAddressesDAL_OrderId",
                table: "OrdersAddressesDAL",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersAddressesDAL");

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

            migrationBuilder.InsertData(
                table: "OrdersAdresses",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000000"), new Guid("60000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "OrdersAdresses",
                columns: new[] { "AddressFromId", "AddressToId", "OrderId", "Id" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAdresses_AddressToId",
                table: "OrdersAdresses",
                column: "AddressToId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersAdresses_OrderId",
                table: "OrdersAdresses",
                column: "OrderId");
        }
    }
}
