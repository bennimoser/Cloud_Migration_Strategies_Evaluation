using System.Web.Http;
using ERP.Data;
using ERP.Data.Repositories;
using ERP.Data.Entitaeten;

namespace ERP.Api.Controllers
{
    public class ArtikelController : ApiController
    {
        private readonly ErpContext _context;
        private readonly ArtikelRepository _repository;

        public ArtikelController()
        {
            _context = new ErpContext();
            _repository = new ArtikelRepository(_context);
        }

        // GET api/artikel
        public IHttpActionResult Get()
        {
            return Ok(_repository.AlleArtikel());
        }

        // GET api/artikel/5
        public IHttpActionResult Get(int id)
        {
            var artikel = _repository.FindById(id);
            if (artikel == null)
                return NotFound();
            return Ok(artikel);
        }

        // POST api/artikel
        public IHttpActionResult Post([FromBody] Artikel artikel)
        {
            if (artikel == null)
                return BadRequest("Artikeldaten fehlen im Request-Body.");
            if (string.IsNullOrWhiteSpace(artikel.Artikelnummer))
                return BadRequest("Artikelnummer ist erforderlich.");
            if (string.IsNullOrWhiteSpace(artikel.Bezeichnung))
                return BadRequest("Bezeichnung ist erforderlich.");
            if (artikel.Verkaufspreis <= 0)
                return BadRequest("Verkaufspreis muss größer als 0 sein.");

            _repository.Hinzufuegen(artikel);
            return Created($"api/artikel/{artikel.Id}", artikel);
        }

        // PUT api/artikel/5
        public IHttpActionResult Put(int id, [FromBody] Artikel artikel)
        {
            if (artikel == null)
                return BadRequest("Artikeldaten fehlen im Request-Body.");

            var existing = _repository.FindById(id);
            if (existing == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(artikel.Artikelnummer))
                return BadRequest("Artikelnummer ist erforderlich.");
            if (string.IsNullOrWhiteSpace(artikel.Bezeichnung))
                return BadRequest("Bezeichnung ist erforderlich.");
            if (artikel.Verkaufspreis <= 0)
                return BadRequest("Verkaufspreis muss größer als 0 sein.");

            existing.Artikelnummer = artikel.Artikelnummer;
            existing.Bezeichnung = artikel.Bezeichnung;
            existing.Verkaufspreis = artikel.Verkaufspreis;

            _repository.Aktualisieren(existing);
            return Ok(existing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
