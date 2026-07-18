using ERP.Data.Entitaeten;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data
{
    public class ErpContext : DbContext
    {
        public ErpContext() : base()
        {
        }

        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Lagerbestand> Lagerbestaende { get; set; }
        public DbSet<Bestellung> Bestellungen { get; set; }
        public DbSet<Bestellposition> Bestellpositionen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artikel>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Artikel>()
                .Property(a => a.Artikelnummer).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Artikel>()
                .Property(a => a.Bezeichnung).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Artikel>()
                .Property(a => a.Verkaufspreis).HasPrecision(18, 2);
            modelBuilder.Entity<Artikel>()
                .HasIndex(a => a.Artikelnummer).IsUnique();

            modelBuilder.Entity<Kunde>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Name).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Anschrift).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Kontaktdaten).HasMaxLength(500);

            modelBuilder.Entity<Lagerbestand>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Lagerbestand>()
                .HasOne(l => l.Artikel)
                .WithMany()
                .HasForeignKey(l => l.ArtikelId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Bestellung>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Bestellung>()
                .HasOne(b => b.Kunde)
                .WithMany(k => k.Bestellungen)
                .HasForeignKey(b => b.KundeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Bestellposition>()
                .HasKey(bp => bp.Id);

            modelBuilder.Entity<Bestellposition>()
                .Property(bp => bp.Einzelpreis).HasPrecision(18, 2);

            modelBuilder.Entity<Bestellposition>()
                .HasOne(bp => bp.Bestellung)
                .WithMany(b => b.Positionen)
                .HasForeignKey(bp => bp.BestellungId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Bestellposition>()
                .HasOne(bp => bp.Artikel)
                .WithMany(a => a.Bestellpositionen)
                .HasForeignKey(bp => bp.ArtikelId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
