using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigAlertas",
                columns: table => new
                {
                    ConfigAlertaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    Ar = table.Column<int>(type: "int", nullable: true),
                    Vento = table.Column<int>(type: "int", nullable: true),
                    Agua = table.Column<int>(type: "int", nullable: true),
                    Solo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigAlertas", x => x.ConfigAlertaId);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    Ar = table.Column<int>(type: "int", nullable: true),
                    Agua = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<int>(type: "int", nullable: true),
                    Longitude = table.Column<int>(type: "int", nullable: true),
                    Vento = table.Column<int>(type: "int", nullable: true),
                    Luz = table.Column<int>(type: "int", nullable: true),
                    Solo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.EquipamentoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAlertas",
                columns: table => new
                {
                    TipoAlertaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAlertas", x => x.TipoAlertaId);
                });

            migrationBuilder.CreateTable(
                name: "EquipamentosClientes",
                columns: table => new
                {
                    EquipamentoClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipamentoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamentosClientes", x => x.EquipamentoClienteId);
                    table.ForeignKey(
                        name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipamentosClientes_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipamentosClientes_Equipamentos_EquipamentoId1",
                        column: x => x.EquipamentoId1,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId");
                });

            migrationBuilder.CreateTable(
                name: "HistoricosEquipamentos",
                columns: table => new
                {
                    HistoricoEquipamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    Ar = table.Column<int>(type: "int", nullable: true),
                    Agua = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<int>(type: "int", nullable: true),
                    Longitude = table.Column<int>(type: "int", nullable: true),
                    Vento = table.Column<int>(type: "int", nullable: true),
                    Luz = table.Column<int>(type: "int", nullable: true),
                    Solo = table.Column<int>(type: "int", nullable: true),
                    DataLeitura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipamentoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosEquipamentos", x => x.HistoricoEquipamentoId);
                    table.ForeignKey(
                        name: "FK_HistoricosEquipamentos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosEquipamentos_Equipamentos_EquipamentoId1",
                        column: x => x.EquipamentoId1,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId");
                });

            migrationBuilder.CreateTable(
                name: "MediasGerais",
                columns: table => new
                {
                    MediaGeralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    MediaTemperatura = table.Column<int>(type: "int", nullable: true),
                    MediaAr = table.Column<int>(type: "int", nullable: true),
                    MediaLuz = table.Column<int>(type: "int", nullable: true),
                    MediaAgua = table.Column<int>(type: "int", nullable: true),
                    MediaSolo = table.Column<int>(type: "int", nullable: true),
                    MediaVento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediasGerais", x => x.MediaGeralId);
                    table.ForeignKey(
                        name: "FK_MediasGerais_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlertasEquipamentos",
                columns: table => new
                {
                    AlertaEquipamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    TipoAlertaId = table.Column<int>(type: "int", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataAlerta = table.Column<DateOnly>(type: "date", nullable: true),
                    TipoAlertaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasEquipamentos", x => x.AlertaEquipamentoId);
                    table.ForeignKey(
                        name: "FK_AlertasEquipamentos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertasEquipamentos_TipoAlertas_TipoAlertaId",
                        column: x => x.TipoAlertaId,
                        principalTable: "TipoAlertas",
                        principalColumn: "TipoAlertaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertasEquipamentos_TipoAlertas_TipoAlertaId1",
                        column: x => x.TipoAlertaId1,
                        principalTable: "TipoAlertas",
                        principalColumn: "TipoAlertaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_EquipamentoId",
                table: "AlertasEquipamentos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_TipoAlertaId",
                table: "AlertasEquipamentos",
                column: "TipoAlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_TipoAlertaId1",
                table: "AlertasEquipamentos",
                column: "TipoAlertaId1");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_EquipamentoId",
                table: "EquipamentosClientes",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_EquipamentoId1",
                table: "EquipamentosClientes",
                column: "EquipamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_UsuarioId",
                table: "EquipamentosClientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEquipamentos_EquipamentoId",
                table: "HistoricosEquipamentos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEquipamentos_EquipamentoId1",
                table: "HistoricosEquipamentos",
                column: "EquipamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_MediasGerais_EquipamentoId",
                table: "MediasGerais",
                column: "EquipamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertasEquipamentos");

            migrationBuilder.DropTable(
                name: "ConfigAlertas");

            migrationBuilder.DropTable(
                name: "EquipamentosClientes");

            migrationBuilder.DropTable(
                name: "HistoricosEquipamentos");

            migrationBuilder.DropTable(
                name: "MediasGerais");

            migrationBuilder.DropTable(
                name: "TipoAlertas");

            migrationBuilder.DropTable(
                name: "Equipamentos");
        }
    }
}
