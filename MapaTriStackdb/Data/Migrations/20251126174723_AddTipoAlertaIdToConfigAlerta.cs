using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoAlertaIdToConfigAlerta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoAlertaId",
                table: "ConfigAlertas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAlertas_TipoAlertaId",
                table: "ConfigAlertas",
                column: "TipoAlertaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigAlertas_TipoAlertas_TipoAlertaId",
                table: "ConfigAlertas",
                column: "TipoAlertaId",
                principalTable: "TipoAlertas",
                principalColumn: "TipoAlertaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigAlertas_TipoAlertas_TipoAlertaId",
                table: "ConfigAlertas");

            migrationBuilder.DropIndex(
                name: "IX_ConfigAlertas_TipoAlertaId",
                table: "ConfigAlertas");

            migrationBuilder.DropColumn(
                name: "TipoAlertaId",
                table: "ConfigAlertas");
        }
    }
}
