using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Data.Entitaeten;

namespace ERP.Data.Repositories
{
    public class BestellungRepository
    {
        private readonly ErpContext _context;

        public BestellungRepository(ErpContext context)
        {
            _context = context;
        }

        public List<Bestellung> AlleBestellungen()
        {
            return _context.Bestellungen
                .Include(b => b.Kunde)
                .Include(b => b.Positionen.Select(p => p.Artikel))
                .OrderByDescending(b => b.Bestelldatum)
                .ToList();
        }

        public Bestellung FindById(int id)
        {
            return _context.Bestellungen
                .Include(b => b.Kunde)
                .Include(b => b.Positionen.Select(p => p.Artikel))
                .FirstOrDefault(b => b.Id == id);
        }
    }
}
