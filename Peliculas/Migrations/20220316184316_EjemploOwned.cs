using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class EjemploOwned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Actores");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
