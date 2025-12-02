using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteToConfigAlerta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClienteId",
                table: "ConfigAlertas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAlertas_ClienteId",
                table: "ConfigAlertas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigAlertas_Clientes_ClienteId",
                table: "ConfigAlertas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigAlertas_Clientes_ClienteId",
                table: "ConfigAlertas");

            migrationBuilder.DropIndex(
                name: "IX_ConfigAlertas_ClienteId",
                table: "ConfigAlertas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "ConfigAlertas");
        }
    }
}
