using System.Collections.Generic;

namespace ERP.Data.Entitaeten
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Artikelnummer { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Verkaufspreis { get; set; }

        public virtual ICollection<Bestellposition> Bestellpositionen { get; set; }

        public Artikel()
        {
            Bestellpositionen = new HashSet<Bestellposition>();
        }
    }
}
