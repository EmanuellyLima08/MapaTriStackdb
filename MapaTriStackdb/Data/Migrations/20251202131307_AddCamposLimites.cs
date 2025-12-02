using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposLimites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vento",
                table: "ConfigAlertas",
                newName: "VentoLimite");

            migrationBuilder.RenameColumn(
                name: "Temperatura",
                table: "ConfigAlertas",
                newName: "TemperaturaLimite");

            migrationBuilder.RenameColumn(
                name: "Solo",
                table: "ConfigAlertas",
                newName: "SoloLimite");

            migrationBuilder.RenameColumn(
                name: "Ar",
                table: "ConfigAlertas",
                newName: "ArLimite");

            migrationBuilder.RenameColumn(
                name: "Agua",
                table: "ConfigAlertas",
                newName: "AguaLimite");

            migrationBuilder.AddColumn<string>(
                name: "Nivel",
                table: "ConfigAlertas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "ConfigAlertas");

            migrationBuilder.RenameColumn(
                name: "VentoLimite",
                table: "ConfigAlertas",
                newName: "Vento");

            migrationBuilder.RenameColumn(
                name: "TemperaturaLimite",
                table: "ConfigAlertas",
                newName: "Temperatura");

            migrationBuilder.RenameColumn(
                name: "SoloLimite",
                table: "ConfigAlertas",
                newName: "Solo");

            migrationBuilder.RenameColumn(
                name: "ArLimite",
                table: "ConfigAlertas",
                newName: "Ar");

            migrationBuilder.RenameColumn(
                name: "AguaLimite",
                table: "ConfigAlertas",
                newName: "Agua");
        }
    }
}
