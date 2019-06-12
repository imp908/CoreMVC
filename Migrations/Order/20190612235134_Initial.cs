﻿using System;
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
                name: "MaterialUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnit", x => x.Id);
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
                    WeightUnitId = table.Column<Guid>(nullable: true),
                    GeometryUnitId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItemParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItemParameters_MaterialUnit_GeometryUnitId",
                        column: x => x.GeometryUnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryItemParameters_MaterialUnit_WeightUnitId",
                        column: x => x.WeightUnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitsConvertion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromId = table.Column<Guid>(nullable: true),
                    ToId = table.Column<Guid>(nullable: true),
                    ConvertionRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsConvertion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitsConvertion_MaterialUnit_FromId",
                        column: x => x.FromId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitsConvertion_MaterialUnit_ToId",
                        column: x => x.ToId,
                        principalTable: "MaterialUnit",
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

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItem_ParametersId",
                table: "DeliveryItem",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemParameters_GeometryUnitId",
                table: "DeliveryItemParameters",
                column: "GeometryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItemParameters_WeightUnitId",
                table: "DeliveryItemParameters",
                column: "WeightUnitId");

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
                name: "MaterialUnit");
        }
    }
}
