namespace ERP.Data.Entitaeten
{
    public class Lagerbestand
    {
        public int Id { get; set; }
        public int Menge { get; set; }
        public int Mindestbestand { get; set; }
        public int ArtikelId { get; set; }

        public virtual Artikel Artikel { get; set; }
    }
}
