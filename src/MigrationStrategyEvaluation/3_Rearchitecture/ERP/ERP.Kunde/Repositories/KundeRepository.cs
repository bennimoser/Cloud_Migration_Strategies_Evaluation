using ERP.Data;
using KundenEntität = ERP.Data.Entitaeten.Kunde;

namespace ERP.Kunde.Repositories
{
    public class KundeRepository
    {
        private readonly ErpContext _context;

        public KundeRepository(ErpContext context)
        {
            _context = context;
        }

        public List<KundenEntität> AlleKunden()
        {
            return _context.Kunden.OrderBy(k => k.Name).ToList();
        }

        public KundenEntität? FindById(int id)
        {
            return _context.Kunden.Find(id);
        }

        public void Hinzufuegen(KundenEntität kunde)
        {
            _context.Kunden.Add(kunde);
            _context.SaveChanges();
        }
    }
}
