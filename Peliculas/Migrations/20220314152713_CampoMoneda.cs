using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class CampoMoneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "SalasDeCines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "SalasDeCines");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
