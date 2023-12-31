﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class HerenciaTPT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchandising",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DisponibleEnInventario = table.Column<bool>(type: "bit", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Volumen = table.Column<double>(type: "float", nullable: false),
                    EsRopa = table.Column<bool>(type: "bit", nullable: false),
                    EsColeccionable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchandising", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchandising_Productos_Id",
                        column: x => x.Id,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PeliculasAlquilables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasAlquilables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeliculasAlquilables_Productos_Id",
                        column: x => x.Id,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "TipoPago",
                value: 1);

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "Precio" },
                values: new object[] { 2, "T-shirt One Piece", 15.0 });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "Precio" },
                values: new object[] { 1, "Spider-Man", 5.9900000000000002 });

            migrationBuilder.InsertData(
                table: "Merchandising",
                columns: new[] { "Id", "DisponibleEnInventario", "EsColeccionable", "EsRopa", "Peso", "Volumen" },
                values: new object[] { 2, true, false, true, 1.0, 1.0 });

            migrationBuilder.InsertData(
                table: "PeliculasAlquilables",
                columns: new[] { "Id", "PeliculaId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchandising");

            migrationBuilder.DropTable(
                name: "PeliculasAlquilables");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "CorreoElectronico", "FechaTransaccion", "Monto", "TipoPago" },
                values: new object[] { 5, "kmlopez@mail.com", new DateTime(2022, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 2 });
        }
    }
}
