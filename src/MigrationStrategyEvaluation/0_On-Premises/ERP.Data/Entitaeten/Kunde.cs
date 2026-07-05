using System.Collections.Generic;

namespace ERP.Data.Entitaeten
{
    public class Kunde
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
        public string Kontaktdaten { get; set; }

        public virtual ICollection<Bestellung> Bestellungen { get; set; }

        public Kunde()
        {
            Bestellungen = new HashSet<Bestellung>();
        }
    }
}
