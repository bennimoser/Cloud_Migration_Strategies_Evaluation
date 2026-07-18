using ERP.Data;
using ERP.Artikel.Repositories;
using Microsoft.AspNetCore.Mvc;
using ArtikelEntity = ERP.Data.Entitaeten.Artikel;

namespace ERP.Api.Controllers
{
    public class ArtikelController : ControllerBase
    {
        private readonly ErpContext _context;
        private readonly ArtikelRepository _repository;

        public ArtikelController(ErpContext context, ArtikelRepository artikelRepository)
        {
            _context = context;
            _repository = artikelRepository;
        }

        // GET api/artikel
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.AlleArtikel());
        }

        // GET api/artikel/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            var artikel = _repository.FindById(id);
            if (artikel == null)
                return NotFound();
            return Ok(artikel);
        }

        // POST api/artikel
        [HttpPost]
        public IActionResult Post([FromBody] ArtikelEntity artikel)
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
        [HttpPut]
        public IActionResult Put(int id, [FromBody] ArtikelEntity artikel)
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
    }
}
