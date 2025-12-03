using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Migrations
{
    /// <inheritdoc />
    public partial class AjusteHistorico_DataRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegistro",
                table: "HistoricosEquipamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRegistro",
                table: "HistoricosEquipamentos");
        }
    }
}
