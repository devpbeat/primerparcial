using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.primerparcial.Migrations
{
    public partial class Latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ciudades_idCiudadId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_idCiudadId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "idCiudadId",
                table: "Clientes",
                newName: "idCiudad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idCiudad",
                table: "Clientes",
                newName: "idCiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_idCiudadId",
                table: "Clientes",
                column: "idCiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ciudades_idCiudadId",
                table: "Clientes",
                column: "idCiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
