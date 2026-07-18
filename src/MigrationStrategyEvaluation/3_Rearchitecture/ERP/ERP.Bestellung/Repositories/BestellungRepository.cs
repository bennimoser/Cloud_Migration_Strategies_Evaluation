using ERP.Data;
using Microsoft.EntityFrameworkCore;
using BestellungEntity = ERP.Data.Entitaeten.Bestellung;

namespace ERP.Bestellung.Repositories
{
    public class BestellungRepository
    {
        private readonly ErpContext _context;

        public BestellungRepository(ErpContext context)
        {
            _context = context;
        }

        public List<BestellungEntity> AlleBestellungen()
        {
            return _context.Bestellungen
                .Include(b => b.Kunde)
                .Include(b => b.Positionen).ThenInclude(p => p.Artikel)
                .OrderByDescending(b => b.Bestelldatum)
                .ToList();
        }

        public BestellungEntity? FindById(int id)
        {
            return _context.Bestellungen
                .Include(b => b.Kunde)
                .Include(b => b.Positionen).ThenInclude(p => p.Artikel)
                .FirstOrDefault(b => b.Id == id);
        }
    }
}
