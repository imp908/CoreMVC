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
                name: "DeliveryItemParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Lenght = table.Column<float>(nullable: false),
                    Height = table.Column<float>(nullable: false),
                    Depth = table.Column<float>(nullable: false),
                    WeightDimensionId = table.Column<Guid>(nullable: false),
                    LenghtDimensionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItemParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItemParameters_DimensionalUnit_LenghtDimensionId",
                        column: x => x.LenghtDimensionId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryItemParameters_DimensionalUnit_WeightDimensionId",
                        column: x => x.WeightDimensionId,
                        principalTable: "DimensionalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "DeliveryItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParametersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItem_DeliveryItemParameters_ParametersId",
                        column: x => x.ParametersId,
                        principalTable: "DeliveryItemParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ItmeId = table.Column<Guid>(nullable: true),
                    FromId = table.Column<Guid>(nullable: true),
                    ToId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    DeliveryPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Address_FromId",
                        column: x => x.FromId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_DeliveryItem_ItmeId",
                        column: x => x.ItmeId,
                        principalTable: "DeliveryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Address_ToId",
                        column: x => x.ToId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "DeliveryItemParameters",
                columns: new[] { "Id", "Depth", "Height", "Lenght", "LenghtDimensionId", "Weight", "WeightDimensionId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000000"), 0f, 0f, 5f, new Guid("00000000-0000-0000-0000-000000000003"), 10f, new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "UnitsConvertion",
                columns: new[] { "Id", "ConvertionRate", "FromId", "ToId" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000000"), 0.220462f, new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "UnitsConvertion",
                columns: new[] { "Id", "ConvertionRate", "FromId", "ToId" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000000"), 2.20462f, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItem_ParametersId",
                table: "DeliveryItem",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemParameters_LenghtDimensionId",
                table: "DeliveryItemParameters",
                column: "LenghtDimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemParameters_WeightDimensionId",
                table: "DeliveryItemParameters",
                column: "WeightDimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_FromId",
                table: "Order",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ItmeId",
                table: "Order",
                column: "ItmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ToId",
                table: "Order",
                column: "ToId");

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
                name: "Order");

            migrationBuilder.DropTable(
                name: "UnitsConvertion");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DeliveryItem");

            migrationBuilder.DropTable(
                name: "DeliveryItemParameters");

            migrationBuilder.DropTable(
                name: "DimensionalUnit");
        }
    }
}
