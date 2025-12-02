using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteIdToEquipamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEquipamentos_AspNetUsers_UsuarioId",
                table: "AlertasEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEquipamentos_Equipamentos_EquipamentoId1",
                table: "AlertasEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_AspNetUsers_UsuarioId",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipamentosClientes_Equipamentos_EquipamentoId1",
                table: "EquipamentosClientes");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosEquipamentos_AspNetUsers_UsuarioId",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosEquipamentos_Equipamentos_EquipamentoId1",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MediasGerais_AspNetUsers_UsuarioId",
                table: "MediasGerais");

            migrationBuilder.DropForeignKey(
                name: "FK_MediasGerais_Equipamentos_EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_MediasGerais_EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropIndex(
                name: "IX_HistoricosEquipamentos_EquipamentoId1",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropIndex(
                name: "IX_EquipamentosClientes_EquipamentoId1",
                table: "EquipamentosClientes");

            migrationBuilder.DropIndex(
                name: "IX_AlertasEquipamentos_EquipamentoId1",
                table: "AlertasEquipamentos");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "MediasGerais");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "EquipamentosClientes");

            migrationBuilder.DropColumn(
                name: "EquipamentoId1",
                table: "AlertasEquipamentos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "MediasGerais",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_MediasGerais_UsuarioId",
                table: "MediasGerais",
                newName: "IX_MediasGerais_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "HistoricosEquipamentos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricosEquipamentos_UsuarioId",
                table: "HistoricosEquipamentos",
                newName: "IX_HistoricosEquipamentos_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "EquipamentosClientes",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipamentosClientes_UsuarioId",
                table: "EquipamentosClientes",
                newName: "IX_EquipamentosClientes_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Equipamentos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipamentos_UsuarioId",
                table: "Equipamentos",
                newName: "IX_Equipamentos_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AlertasEquipamentos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_AlertasEquipamentos_UsuarioId",
                table: "AlertasEquipamentos",
                newName: "IX_AlertasEquipamentos_ClienteId");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AlertasEquipamentos_Clientes_ClienteId",
                table: "AlertasEquipamentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Clientes_ClienteId",
                table: "Equipamentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipamentosClientes_Clientes_ClienteId",
                table: "EquipamentosClientes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosEquipamentos_Clientes_ClienteId",
                table: "HistoricosEquipamentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediasGerais_Clientes_ClienteId",
                table: "MediasGerais",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertasEquipamentos_Clientes_ClienteId",
                table: "AlertasEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Clientes_ClienteId",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipamentosClientes_Clientes_ClienteId",
                table: "EquipamentosClientes");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosEquipamentos_Clientes_ClienteId",
                table: "HistoricosEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MediasGerais_Clientes_ClienteId",
                table: "MediasGerais");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "MediasGerais",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_MediasGerais_ClienteId",
                table: "MediasGerais",
                newName: "IX_MediasGerais_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "HistoricosEquipamentos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricosEquipamentos_ClienteId",
                table: "HistoricosEquipamentos",
                newName: "IX_HistoricosEquipamentos_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "EquipamentosClientes",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipamentosClientes_ClienteId",
                table: "EquipamentosClientes",
                newName: "IX_EquipamentosClientes_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Equipamentos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipamentos_ClienteId",
                table: "Equipamentos",
                newName: "IX_Equipamentos_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "AlertasEquipamentos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AlertasEquipamentos_ClienteId",
                table: "AlertasEquipamentos",
                newName: "IX_AlertasEquipamentos_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId1",
                table: "MediasGerais",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId1",
                table: "HistoricosEquipamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId1",
                table: "EquipamentosClientes",
                type: "int",
                nullable: true);

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
                name: "IX_HistoricosEquipamentos_EquipamentoId1",
                table: "HistoricosEquipamentos",
                column: "EquipamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_EquipamentoId1",
                table: "EquipamentosClientes",
                column: "EquipamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_EquipamentoId1",
                table: "AlertasEquipamentos",
                column: "EquipamentoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertasEquipamentos_AspNetUsers_UsuarioId",
                table: "AlertasEquipamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_EquipamentosClientes_AspNetUsers_UsuarioId",
                table: "EquipamentosClientes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipamentosClientes_Equipamentos_EquipamentoId1",
                table: "EquipamentosClientes",
                column: "EquipamentoId1",
                principalTable: "Equipamentos",
                principalColumn: "EquipamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosEquipamentos_AspNetUsers_UsuarioId",
                table: "HistoricosEquipamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosEquipamentos_Equipamentos_EquipamentoId1",
                table: "HistoricosEquipamentos",
                column: "EquipamentoId1",
                principalTable: "Equipamentos",
                principalColumn: "EquipamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediasGerais_AspNetUsers_UsuarioId",
                table: "MediasGerais",
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
    }
}
