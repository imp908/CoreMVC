using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.Order
{
    public partial class OrderPropsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DaysToDelivery",
                table: "Order",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToDelivery",
                table: "Order");
        }
    }
}
