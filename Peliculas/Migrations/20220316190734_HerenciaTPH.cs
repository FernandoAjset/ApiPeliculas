using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class HerenciaTPH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "date", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UltimosDigitos = table.Column<string>(type: "char(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "CorreoElectronico", "FechaTransaccion", "Monto", "TipoPago" },
                values: new object[,]
                {
                    { 3, "hgarcia@mail.com", new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, 1 },
                    { 5, "kmlopez@mail.com", new DateTime(2022, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "Monto", "TipoPago", "UltimosDigitos" },
                values: new object[,]
                {
                    { 8, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, 2, "0123" },
                    { 15, new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 2, "2468" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");
        }
    }
}
