using ERP.Lagerstand.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers
{
    public class LagerbestandController : ControllerBase
    {
        private readonly LagerbestandRepository _repository;

        public LagerbestandController(LagerbestandRepository lagerbestandRepository)
        {
            _repository = lagerbestandRepository;
        }

        // GET api/lager/5  (id = artikelId)
        [HttpGet]
        public IActionResult Get(int id)
        {
            var lagerbestand = _repository.FindByArtikelId(id);
            if (lagerbestand == null)
                return NotFound();
            return Ok(lagerbestand);
        }

        // PUT api/lager/5  (id = artikelId)
        [HttpPut]
        public IActionResult Put(int id, [FromBody] LagerbestandUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Lagerbestandsdaten fehlen im Request-Body.");
            if (dto.Menge < 0)
                return BadRequest("Menge darf nicht negativ sein.");
            if (dto.Mindestbestand < 0)
                return BadRequest("Mindestbestand darf nicht negativ sein.");

            var lagerbestand = _repository.FindByArtikelId(id);
            if (lagerbestand == null)
                return NotFound();

            lagerbestand.Menge = dto.Menge;
            lagerbestand.Mindestbestand = dto.Mindestbestand;

            _repository.Aktualisieren(lagerbestand);
            return Ok(lagerbestand);
        }
    }

    public class LagerbestandUpdateDto
    {
        public int Menge { get; set; }
        public int Mindestbestand { get; set; }
    }
}
