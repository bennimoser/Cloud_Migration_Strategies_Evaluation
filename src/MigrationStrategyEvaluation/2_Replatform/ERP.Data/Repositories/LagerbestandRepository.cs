using System.Data.Entity;
using System.Linq;
using ERP.Data.Entitaeten;

namespace ERP.Data.Repositories
{
    public class LagerbestandRepository
    {
        private readonly ErpContext _context;

        public LagerbestandRepository(ErpContext context)
        {
            _context = context;
        }

        public Lagerbestand FindByArtikelId(int artikelId)
        {
            return _context.Lagerbestaende
                .Include(l => l.Artikel)
                .FirstOrDefault(l => l.ArtikelId == artikelId);
        }

        public void Aktualisieren(Lagerbestand lagerbestand)
        {
            _context.Entry(lagerbestand).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
