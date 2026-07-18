namespace ERP.Data.Entitaeten
{
    public class Bestellposition
    {
        public int Id { get; set; }
        public int Menge { get; set; }
        public decimal Einzelpreis { get; set; }
        public int BestellungId { get; set; }
        public int ArtikelId { get; set; }

        public virtual Bestellung Bestellung { get; set; }
        public virtual Artikel Artikel { get; set; }
    }
}
