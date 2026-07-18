using System;
using System.Collections.Generic;

namespace ERP.Data.Entitaeten
{
    public class Bestellung
    {
        public int Id { get; set; }
        public DateTime Bestelldatum { get; set; }
        public int KundeId { get; set; }

        public virtual Kunde Kunde { get; set; }
        public virtual ICollection<Bestellposition> Positionen { get; set; }

        public Bestellung()
        {
            Positionen = new HashSet<Bestellposition>();
        }
    }
}
