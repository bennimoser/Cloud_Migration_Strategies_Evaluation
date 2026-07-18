using System.Data.Entity;
using ERP.Data.Entitaeten;

namespace ERP.Data
{
    public class ErpContext : DbContext
    {
        public ErpContext() : base("name=ErpContext")
        {
        }

        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Lagerbestand> Lagerbestaende { get; set; }
        public DbSet<Bestellung> Bestellungen { get; set; }
        public DbSet<Bestellposition> Bestellpositionen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artikel>()
                .ToTable("Artikel")
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
                .ToTable("Kunden")
                .HasKey(k => k.Id);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Name).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Anschrift).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Kunde>()
                .Property(k => k.Kontaktdaten).HasMaxLength(500);

            modelBuilder.Entity<Lagerbestand>()
                .ToTable("Lagerbestaende")
                .HasKey(l => l.Id);
            modelBuilder.Entity<Lagerbestand>()
                .HasRequired(l => l.Artikel)
                .WithMany()
                .HasForeignKey(l => l.ArtikelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bestellung>()
                .ToTable("Bestellungen")
                .HasKey(b => b.Id);
            modelBuilder.Entity<Bestellung>()
                .HasRequired(b => b.Kunde)
                .WithMany(k => k.Bestellungen)
                .HasForeignKey(b => b.KundeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bestellposition>()
                .ToTable("Bestellpositionen")
                .HasKey(bp => bp.Id);
            modelBuilder.Entity<Bestellposition>()
                .Property(bp => bp.Einzelpreis).HasPrecision(18, 2);
            modelBuilder.Entity<Bestellposition>()
                .HasRequired(bp => bp.Bestellung)
                .WithMany(b => b.Positionen)
                .HasForeignKey(bp => bp.BestellungId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Bestellposition>()
                .HasRequired(bp => bp.Artikel)
                .WithMany(a => a.Bestellpositionen)
                .HasForeignKey(bp => bp.ArtikelId)
                .WillCascadeOnDelete(false);
        }
    }
}
