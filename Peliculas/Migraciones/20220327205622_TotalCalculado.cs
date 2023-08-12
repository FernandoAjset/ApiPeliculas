using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class TotalCalculado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "FacturaDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "FacturaDetalles",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "Precio * Cantidad");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaFin",
                value: new DateTime(2022, 4, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaFin",
                value: new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "FacturaDetalles");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "FacturaDetalles");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaFin",
                value: new DateTime(2022, 3, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaFin",
                value: new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
