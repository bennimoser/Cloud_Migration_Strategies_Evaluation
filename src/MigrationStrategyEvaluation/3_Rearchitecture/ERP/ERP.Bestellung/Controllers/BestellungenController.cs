using ERP.Bestellung.Models;
using ERP.Bestellung.Repositories;
using ERP.Bestellung.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers
{
    [ApiController]
    [Route("api/bestellungen")]
    public class BestellungenController : ControllerBase
    {
        private readonly BestellungRepository _repository;
        private readonly Bestellabwicklung _bestellabwicklung;

        public BestellungenController(BestellungRepository bestellungRepository, Bestellabwicklung bestellabwicklung)
        {
            _repository = bestellungRepository;
            _bestellabwicklung = bestellabwicklung;
        }

        // GET api/bestellungen
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.AlleBestellungen());
        }

        // GET api/bestellungen/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bestellung = _repository.FindById(id);
            if (bestellung == null)
                return NotFound();
            return Ok(bestellung);
        }

        // POST api/bestellungen
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BestellungAnfrageDto anfrage)
        {
            if (anfrage == null)
                return BadRequest("Bestelldaten fehlen im Request-Body.");
            if (anfrage.Positionen == null || anfrage.Positionen.Count == 0)
                return BadRequest("Die Bestellung enthält keine Positionen.");

            try
            {
                int bestellungId = await _bestellabwicklung.BestellungAufgeben(anfrage);
                return Created($"api/bestellungen/{bestellungId}", new { id = bestellungId });
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
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { fehler = $"Fehler bei der Kommunikation mit einem Microservice: {ex.Message}" });
            }
        }
    }
}
