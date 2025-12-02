using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoEquipamentoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "EquipamentosClientes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "EquipamentosClientes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
