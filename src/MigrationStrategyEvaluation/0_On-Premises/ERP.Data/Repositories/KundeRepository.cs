using System.Collections.Generic;
using System.Linq;
using ERP.Data.Entitaeten;

namespace ERP.Data.Repositories
{
    public class KundeRepository
    {
        private readonly ErpContext _context;

        public KundeRepository(ErpContext context)
        {
            _context = context;
        }

        public List<Kunde> AlleKunden()
        {
            return _context.Kunden.OrderBy(k => k.Name).ToList();
        }

        public Kunde FindById(int id)
        {
            return _context.Kunden.Find(id);
        }

        public void Hinzufuegen(Kunde kunde)
        {
            _context.Kunden.Add(kunde);
            _context.SaveChanges();
        }
    }
}
