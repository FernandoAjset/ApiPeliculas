using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class CineDetalleTableSplitting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines");

            migrationBuilder.AddColumn<string>(
                name: "CodigoDeEtica",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Historia",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mision",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Valores",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines",
                column: "CineIdentificador",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines");

            migrationBuilder.DropColumn(
                name: "CodigoDeEtica",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Historia",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Mision",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Valores",
                table: "Cines");

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCines_Cines_CineIdentificador",
                table: "SalasDeCines",
                column: "CineIdentificador",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
