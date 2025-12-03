using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapaTriStackdb.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Solo = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.EquipamentoId);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigAlertas",
                columns: table => new
                {
                    ConfigAlertaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoAlertaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TemperaturaLimite = table.Column<int>(type: "int", nullable: true),
                    ArLimite = table.Column<int>(type: "int", nullable: true),
                    VentoLimite = table.Column<int>(type: "int", nullable: true),
                    AguaLimite = table.Column<int>(type: "int", nullable: true),
                    SoloLimite = table.Column<int>(type: "int", nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigAlertas", x => x.ConfigAlertaId);
                    table.ForeignKey(
                        name: "FK_ConfigAlertas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigAlertas_TipoAlertas_TipoAlertaId",
                        column: x => x.TipoAlertaId,
                        principalTable: "TipoAlertas",
                        principalColumn: "TipoAlertaId",
                        onDelete: ReferentialAction.Cascade);
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
                    DataAlerta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TipoAlertaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasEquipamentos", x => x.AlertaEquipamentoId);
                    table.ForeignKey(
                        name: "FK_AlertasEquipamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "EquipamentosClientes",
                columns: table => new
                {
                    EquipamentoClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamentosClientes", x => x.EquipamentoClienteId);
                    table.ForeignKey(
                        name: "FK_EquipamentosClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipamentosClientes_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
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
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosEquipamentos", x => x.HistoricoEquipamentoId);
                    table.ForeignKey(
                        name: "FK_HistoricosEquipamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosEquipamentos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
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
                    MediaVento = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediasGerais", x => x.MediaGeralId);
                    table.ForeignKey(
                        name: "FK_MediasGerais_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediasGerais_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertasEquipamentos_ClienteId",
                table: "AlertasEquipamentos",
                column: "ClienteId");

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAlertas_ClienteId",
                table: "ConfigAlertas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAlertas_TipoAlertaId",
                table: "ConfigAlertas",
                column: "TipoAlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteId",
                table: "Equipamentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_ClienteId",
                table: "EquipamentosClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosClientes_EquipamentoId",
                table: "EquipamentosClientes",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEquipamentos_ClienteId",
                table: "HistoricosEquipamentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEquipamentos_EquipamentoId",
                table: "HistoricosEquipamentos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MediasGerais_ClienteId",
                table: "MediasGerais",
                column: "ClienteId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ConfigAlertas");

            migrationBuilder.DropTable(
                name: "EquipamentosClientes");

            migrationBuilder.DropTable(
                name: "HistoricosEquipamentos");

            migrationBuilder.DropTable(
                name: "MediasGerais");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TipoAlertas");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
