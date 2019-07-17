using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeliveryName = table.Column<string>(nullable: true),
                    DeliveryNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalDimensions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParameterName = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    DimensionUnitId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalDimensions_PhysicalUnits_DimensionUnitId",
                        column: x => x.DimensionUnitId,
                        principalTable: "PhysicalUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteVertexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InRouteMoveOrder = table.Column<int>(nullable: false),
                    FromId = table.Column<Guid>(nullable: true),
                    ToId = table.Column<Guid>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    PriorityWeigth = table.Column<int>(nullable: false),
                    RouteDALId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteVertexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteVertexes_Adresses_FromId",
                        column: x => x.FromId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVertexes_Routes_RouteDALId",
                        column: x => x.RouteDALId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVertexes_Adresses_ToId",
                        column: x => x.ToId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalDimensions_DimensionUnitId",
                table: "PhysicalDimensions",
                column: "DimensionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVertexes_FromId",
                table: "RouteVertexes",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVertexes_RouteDALId",
                table: "RouteVertexes",
                column: "RouteDALId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVertexes_ToId",
                table: "RouteVertexes",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PhysicalDimensions");

            migrationBuilder.DropTable(
                name: "RouteVertexes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PhysicalUnits");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
