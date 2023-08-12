using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class SalaDeCineForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCines_Cines_CineId",
                table: "SalasDeCines");

            migrationBuilder.RenameColumn(
                name: "CineId",
                table: "SalasDeCines",
                newName: "CineIdentificador");

            migrationBuilder.RenameIndex(
                name: "IX_SalasDeCines_CineId",
                table: "SalasDeCines",
                newName: "IX_SalasDeCines_CineIdentificador");

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

            migrationBuilder.RenameColumn(
                name: "CineIdentificador",
                table: "SalasDeCines",
                newName: "CineId");

            migrationBuilder.RenameIndex(
                name: "IX_SalasDeCines_CineIdentificador",
                table: "SalasDeCines",
                newName: "IX_SalasDeCines_CineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCines_Cines_CineId",
                table: "SalasDeCines",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
