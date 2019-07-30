using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvccoresb.Migrations.CostControll
{
    public partial class Attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dds");

            migrationBuilder.CreateTable(
                name: "Corporate_Card",
                schema: "dic",
                columns: table => new
                {
                    Id_Corporate_Card = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id_Individual_Person = table.Column<int>(nullable: false),
                    Account_Number = table.Column<string>(nullable: true),
                    Date_Open = table.Column<DateTime>(nullable: false),
                    Date_Close = table.Column<DateTime>(nullable: true),
                    Id_Status = table.Column<int>(nullable: false),
                    Limit = table.Column<decimal>(nullable: true),
                    Id_Currency = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Is_Use_In_Request = table.Column<bool>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Modified_By = table.Column<int>(nullable: true),
                    Modified_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporate_Card", x => x.Id_Corporate_Card);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary",
                schema: "dic",
                columns: table => new
                {
                    Id_Dictionary = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id_Dictionary);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary_Attachment",
                schema: "dds",
                columns: table => new
                {
                    Id_Dictionary_Attachment = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dictionary_Type = table.Column<string>(nullable: true),
                    Id_File = table.Column<long>(nullable: true),
                    Rowversion = table.Column<byte[]>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Id_Attachment_Kind = table.Column<int>(nullable: true),
                    Link_Url = table.Column<string>(nullable: true),
                    Link_Name = table.Column<string>(nullable: true),
                    CorporateCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary_Attachment", x => x.Id_Dictionary_Attachment);
                    table.ForeignKey(
                        name: "FK_Dictionary_Attachment_Corporate_Card_CorporateCardId",
                        column: x => x.CorporateCardId,
                        principalSchema: "dic",
                        principalTable: "Corporate_Card",
                        principalColumn: "Id_Corporate_Card",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary_Dictionary_Attachment",
                schema: "dds",
                columns: table => new
                {
                    Id_Dictionary_Dictionary_Attachment = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id_Attachment = table.Column<long>(nullable: false),
                    Id_Dictinary_Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary_Dictionary_Attachment", x => x.Id_Dictionary_Dictionary_Attachment);
                    table.ForeignKey(
                        name: "FK_Dictionary_Dictionary_Attachment_Dictionary_Attachment_Id_Attachment",
                        column: x => x.Id_Attachment,
                        principalSchema: "dds",
                        principalTable: "Dictionary_Attachment",
                        principalColumn: "Id_Dictionary_Attachment",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dictionary_Dictionary_Attachment_Dictionary_Id_Dictinary_Type",
                        column: x => x.Id_Dictinary_Type,
                        principalSchema: "dic",
                        principalTable: "Dictionary",
                        principalColumn: "Id_Dictionary",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_Attachment_CorporateCardId",
                schema: "dds",
                table: "Dictionary_Attachment",
                column: "CorporateCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_Dictionary_Attachment_Id_Attachment",
                schema: "dds",
                table: "Dictionary_Dictionary_Attachment",
                column: "Id_Attachment");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_Dictionary_Attachment_Id_Dictinary_Type",
                schema: "dds",
                table: "Dictionary_Dictionary_Attachment",
                column: "Id_Dictinary_Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dictionary_Dictionary_Attachment",
                schema: "dds");

            migrationBuilder.DropTable(
                name: "Dictionary_Attachment",
                schema: "dds");

            migrationBuilder.DropTable(
                name: "Dictionary",
                schema: "dic");

            migrationBuilder.DropTable(
                name: "Corporate_Card",
                schema: "dic");
        }
    }
}
