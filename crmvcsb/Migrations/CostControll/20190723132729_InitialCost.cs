using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crmvcsb.Migrations.CostControll
{
    public partial class InitialCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dic");

            migrationBuilder.CreateTable(
                name: "Business_Columns",
                schema: "dic",
                columns: table => new
                {
                    Id_Business_Column = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Status_Id = table.Column<int>(nullable: false),
                    Is_Use_In_Request = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business_Columns", x => x.Id_Business_Column);
                });

            migrationBuilder.CreateTable(
                name: "Business_Lines",
                schema: "dic",
                columns: table => new
                {
                    Id_Business_Line = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Status_Id = table.Column<int>(nullable: false),
                    Is_Use_In_Request = table.Column<string>(nullable: true),
                    BusinessColumnId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business_Lines", x => x.Id_Business_Line);
                    table.ForeignKey(
                        name: "FK_Business_Lines_Business_Columns_BusinessColumnId",
                        column: x => x.BusinessColumnId,
                        principalSchema: "dic",
                        principalTable: "Business_Columns",
                        principalColumn: "Id_Business_Column",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_Lines_BusinessColumnId",
                schema: "dic",
                table: "Business_Lines",
                column: "BusinessColumnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business_Lines",
                schema: "dic");

            migrationBuilder.DropTable(
                name: "Business_Columns",
                schema: "dic");
        }
    }
}
