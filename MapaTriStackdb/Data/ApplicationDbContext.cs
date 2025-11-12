using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapaTriStackdb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TipoAlerta> TipoAlertas { get; set; }
        public DbSet<ConfigAlerta> ConfigAlertas { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<EquipamentoCliente> EquipamentosClientes { get; set; }
        public DbSet<AlertaEquipamento> AlertasEquipamentos { get; set; }
        public DbSet<HistoricoEquipamento> HistoricosEquipamentos { get; set; }
        public DbSet<MediaGeral> MediasGerais { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔸 Nome das tabelas no banco
            builder.Entity<TipoAlerta>().ToTable("TipoAlertas");
            builder.Entity<ConfigAlerta>().ToTable("ConfigAlertas");
            builder.Entity<Equipamento>().ToTable("Equipamentos");
            builder.Entity<EquipamentoCliente>().ToTable("EquipamentosClientes");
            builder.Entity<AlertaEquipamento>().ToTable("AlertasEquipamentos");
            builder.Entity<HistoricoEquipamento>().ToTable("HistoricosEquipamentos");
            builder.Entity<MediaGeral>().ToTable("MediasGerais");

            // 🔸 Relacionamentos
            builder.Entity<EquipamentoCliente>()
                .HasOne(ec => ec.Equipamento)
                .WithMany()
                .HasForeignKey(ec => ec.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AlertaEquipamento>()
                .HasOne(ae => ae.Equipamento)
                .WithMany()
                .HasForeignKey(ae => ae.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AlertaEquipamento>()
                .HasOne(ae => ae.TipoAlerta)
                .WithMany()
                .HasForeignKey(ae => ae.TipoAlertaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HistoricoEquipamento>()
                .HasOne(h => h.Equipamento)
                .WithMany()
                .HasForeignKey(h => h.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MediaGeral>()
                .HasOne(m => m.Equipamento)
                .WithMany()
                .HasForeignKey(m => m.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
