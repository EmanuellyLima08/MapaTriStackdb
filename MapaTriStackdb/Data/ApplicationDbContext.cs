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

        // 📌 Tabelas
        public DbSet<Cliente> Clientes { get; set; }
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

            // 🔸 Nome das tabelas
            builder.Entity<Cliente>().ToTable("Clientes");
            builder.Entity<TipoAlerta>().ToTable("TipoAlertas");
            builder.Entity<ConfigAlerta>().ToTable("ConfigAlertas");
            builder.Entity<Equipamento>().ToTable("Equipamentos");
            builder.Entity<EquipamentoCliente>().ToTable("EquipamentosClientes");
            builder.Entity<AlertaEquipamento>().ToTable("AlertasEquipamentos");
            builder.Entity<HistoricoEquipamento>().ToTable("HistoricosEquipamentos");
            builder.Entity<MediaGeral>().ToTable("MediasGerais");

            // 🔸 Relações

            // ------------------------------
            // EquipamentoCliente → Cliente
            // ------------------------------
            builder.Entity<EquipamentoCliente>()
                .HasOne(ec => ec.Cliente)
                .WithMany(c => c.EquipamentosClientes)
                .HasForeignKey(ec => ec.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // EquipamentoCliente → Equipamento
            // ------------------------------
            builder.Entity<EquipamentoCliente>()
                .HasOne(ec => ec.Equipamento)
                .WithMany(e => e.EquipamentosClientes)
                .HasForeignKey(ec => ec.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // AlertaEquipamento → Equipamento
            // ------------------------------
            builder.Entity<AlertaEquipamento>()
                .HasOne(ae => ae.Equipamento)
                .WithMany(e => e.AlertasEquipamento)
                .HasForeignKey(ae => ae.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // AlertaEquipamento → Cliente
            // ------------------------------
            builder.Entity<AlertaEquipamento>()
                .HasOne(ae => ae.Cliente)
                .WithMany(c => c.Alertas)
                .HasForeignKey(ae => ae.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // AlertaEquipamento → TipoAlerta
            // ------------------------------
            builder.Entity<AlertaEquipamento>()
                .HasOne(ae => ae.TipoAlerta)
                .WithMany()
                .HasForeignKey(ae => ae.TipoAlertaId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // HistoricoEquipamento → Equipamento
            // ------------------------------
            builder.Entity<HistoricoEquipamento>()
                .HasOne(h => h.Equipamento)
                .WithMany(e => e.Historicos)
                .HasForeignKey(h => h.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // HistoricoEquipamento → Cliente
            // ------------------------------
            builder.Entity<HistoricoEquipamento>()
                .HasOne(h => h.Cliente)
                .WithMany(c => c.Historicos)
                .HasForeignKey(h => h.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // MediaGeral → Equipamento
            // ------------------------------
            builder.Entity<MediaGeral>()
                .HasOne(m => m.Equipamento)
                .WithMany(e => e.MediasGerais)
                .HasForeignKey(m => m.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // MediaGeral → Cliente
            // ------------------------------
            builder.Entity<MediaGeral>()
                .HasOne(m => m.Cliente)
                .WithMany(c => c.MediasGerais)
                .HasForeignKey(m => m.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------
            // Equipamento → Cliente (Proprietário)
            // ------------------------------
            builder.Entity<Equipamento>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Equipamentos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
