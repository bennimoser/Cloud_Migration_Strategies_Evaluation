using ERP.Data;
using Microsoft.EntityFrameworkCore;
using ArtikelEntity = ERP.Data.Entitaeten.Artikel;


namespace ERP.Artikel.Repositories
{
    public class ArtikelRepository
    {
        private readonly ErpContext _context;

        public ArtikelRepository(ErpContext context)
        {
            _context = context;
        }

        public List<ArtikelEntity> AlleArtikel()
        {
            return _context.Artikel.OrderBy(a => a.Artikelnummer).ToList();
        }

        public ArtikelEntity FindById(int id)
        {
            return _context.Artikel.Find(id);
        }

        public void Hinzufuegen(ArtikelEntity artikel)
        {
            _context.Artikel.Add(artikel);
            _context.SaveChanges();
        }

        public void Aktualisieren(ArtikelEntity artikel)
        {
            _context.Entry(artikel).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
