using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.Order
{
    public partial class CountPropsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToDeliver",
                table: "OrdersAdresses");

            migrationBuilder.DropColumn(
                name: "ItemsOrderedAmount",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "DeliveryItemDimensionUnitDAL",
                newName: "UnitAmount");

            migrationBuilder.AlterColumn<float>(
                name: "DeliveryPrice",
                table: "Order",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "DaysToDelivery",
                table: "Order",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000000"),
                columns: new[] { "DaysToDelivery", "DeliveryPrice" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                columns: new[] { "DaysToDelivery", "DeliveryPrice" },
                values: new object[] { null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitAmount",
                table: "DeliveryItemDimensionUnitDAL",
                newName: "Amount");

            migrationBuilder.AddColumn<DateTime>(
                name: "DaysToDeliver",
                table: "OrdersAdresses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<float>(
                name: "DeliveryPrice",
                table: "Order",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "DaysToDelivery",
                table: "Order",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemsOrderedAmount",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000000"),
                columns: new[] { "DaysToDelivery", "DeliveryPrice" },
                values: new object[] { 0f, 0f });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                columns: new[] { "DaysToDelivery", "DeliveryPrice" },
                values: new object[] { 0f, 0f });
        }
    }
}
