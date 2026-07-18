using ERP.Bestellung.Models;
using ERP.Bestellung.Repositories;
using ERP.Bestellung.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers
{
    public class BestellungenController : ControllerBase
    {
        private readonly BestellungRepository _repository;
        private readonly Bestellabwicklung _bestellabwicklung;

        public BestellungenController(BestellungRepository bestellungRepository, Bestellabwicklung bestellabwicklung)
        {
            _repository = bestellungRepository;
            _bestellabwicklung = bestellabwicklung;
        }

        // GET api/bestellung
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.AlleBestellungen());
        }

        // GET api/bestellung/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            var bestellung = _repository.FindById(id);
            if (bestellung == null)
                return NotFound();
            return Ok(bestellung);
        }

        // POST api/bestellung
        [HttpPost]
        public IActionResult Post([FromBody] BestellungAnfrageDto anfrage)
        {
            if (anfrage == null)
                return BadRequest("Bestelldaten fehlen im Request-Body.");
            if (anfrage.Positionen == null || anfrage.Positionen.Count == 0)
                return BadRequest("Die Bestellung enthält keine Positionen.");

            try
            {
                int bestellungId = _bestellabwicklung.BestellungAufgeben(anfrage);
                return Created($"api/bestellung/{bestellungId}", new { id = bestellungId });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { fehler = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { fehler = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { fehler = ex.Message });
            }
        }
    }
}
