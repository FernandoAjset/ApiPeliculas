using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class CambioFechaSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaInicio",
                value: new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaInicio",
                value: new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2021, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaInicio",
                value: new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaInicio",
                value: new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
