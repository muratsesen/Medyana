using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klinik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klinik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ekipman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TeminTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    BirimFiyat = table.Column<float>(type: "real", nullable: false),
                    KullanimOrani = table.Column<float>(type: "real", nullable: false),
                    KlinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipman", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekipman_Klinik_KlinikId",
                        column: x => x.KlinikId,
                        principalTable: "Klinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ekipman_KlinikId",
                table: "Ekipman",
                column: "KlinikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ekipman");

            migrationBuilder.DropTable(
                name: "Klinik");
        }
    }
}
