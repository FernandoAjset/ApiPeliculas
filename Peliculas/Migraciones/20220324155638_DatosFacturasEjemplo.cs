using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class DatosFacturasEjemplo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines");

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

            migrationBuilder.InsertData(
                table: "Facturas",
                columns: new[] { "Id", "FechaCreacion" },
                values: new object[,]
                {
                    { 4, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2022, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "FacturaDetalles",
                columns: new[] { "Id", "FacturaId", "Precio", "Producto" },
                values: new object[,]
                {
                    { 6, 4, 40m, "Combo nachos" },
                    { 7, 4, 60m, "Combo especial" },
                    { 8, 4, 55m, "Combo dulces" },
                    { 9, 5, 40m, "Combo extraordinario" },
                    { 10, 5, 30m, "Combo fiestamix" },
                    { 11, 5, 65m, "Combo picante" },
                    { 12, 6, 25m, "Combo amigos" },
                    { 13, 6, 60m, "Combo picante" },
                    { 14, 6, 75m, "Combo dulces" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines",
                column: "CineIdentificador",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines");

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaFin",
                value: new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaFin",
                value: new DateTime(2022, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines",
                column: "CineIdentificador",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
