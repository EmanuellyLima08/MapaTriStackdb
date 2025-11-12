using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId1",
                table: "MediasGerais",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Equipamentos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId1",
                table: "AlertasEquipamentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediasGerais_EquipamentoId1",
                table: "MediasGerais",
                column: "EquipamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_UsuarioId",
                table: "Equipamentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_EquipamentoId1",
                table: "AlertasEquipamentos",
                column: "EquipamentoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertasEquipamentos_Equipamentos_EquipamentoId1",
                table: "AlertasEquipamentos",
                column: "EquipamentoId1",
                principalTable: "Equipamentos",
                principalColumn: "EquipamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_AspNetUsers_UsuarioId",
                table: "Equipamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediasGerais_Equipamentos_EquipamentoId1",
                table: "MediasGerais",
                column: "EquipamentoId1",
                principalTable: "Equipamentos",
                principalColumn: "EquipamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEquipamentos_Equipamentos_EquipamentoId1",
                table: "AlertasEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_AspNetUsers_UsuarioId",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MediasGerais_Equipamentos_EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_MediasGerais_EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_UsuarioId",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_AlertasEquipamentos_EquipamentoId1",
                table: "AlertasEquipamentos");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "AlertasEquipamentos");
        }
    }
}
