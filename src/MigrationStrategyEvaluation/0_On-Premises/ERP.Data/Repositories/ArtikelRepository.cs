using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Data.Entitaeten;

namespace ERP.Data.Repositories
{
    public class ArtikelRepository
    {
        private readonly ErpContext _context;

        public ArtikelRepository(ErpContext context)
        {
            _context = context;
        }

        public List<Artikel> AlleArtikel()
        {
            return _context.Artikel.OrderBy(a => a.Artikelnummer).ToList();
        }

        public Artikel FindById(int id)
        {
            return _context.Artikel.Find(id);
        }

        public void Hinzufuegen(Artikel artikel)
        {
            _context.Artikel.Add(artikel);
            _context.SaveChanges();
        }

        public void Aktualisieren(Artikel artikel)
        {
            _context.Entry(artikel).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
