using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "MediasGerais",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "HistoricosEquipamentos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "AlertasEquipamentos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MediasGerais_UsuarioId",
                table: "MediasGerais",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEquipamentos_UsuarioId",
                table: "HistoricosEquipamentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_UsuarioId",
                table: "AlertasEquipamentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertasEquipamentos_AspNetUsers_UsuarioId",
                table: "AlertasEquipamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosEquipamentos_AspNetUsers_UsuarioId",
                table: "HistoricosEquipamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediasGerais_AspNetUsers_UsuarioId",
                table: "MediasGerais",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEquipamentos_AspNetUsers_UsuarioId",
                table: "AlertasEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosEquipamentos_AspNetUsers_UsuarioId",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MediasGerais_AspNetUsers_UsuarioId",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_MediasGerais_UsuarioId",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_HistoricosEquipamentos_UsuarioId",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropIndex(
                name: "IX_AlertasEquipamentos_UsuarioId",
                table: "AlertasEquipamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "MediasGerais");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AlertasEquipamentos");
        }
    }
}
