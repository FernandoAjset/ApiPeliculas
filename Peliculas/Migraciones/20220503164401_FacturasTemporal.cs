using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class FacturasTemporal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Facturas")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "FacturasHistorico")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.AddColumn<DateTime>(
                name: "Desde",
                table: "Facturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.AddColumn<DateTime>(
                name: "Hasta",
                table: "Facturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaFin",
                value: new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaFin",
                value: new DateTime(2022, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desde",
                table: "Facturas")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "FacturasHistorico")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.DropColumn(
                name: "Hasta",
                table: "Facturas")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "FacturasHistorico")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.AlterTable(
                name: "Facturas")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "FacturasHistorico")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "Hasta")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "Desde");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaFin",
                value: new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaFin",
                value: new DateTime(2022, 4, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
