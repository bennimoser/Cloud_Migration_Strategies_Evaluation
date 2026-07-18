using ERP.Kunde.Repositories;
using Microsoft.AspNetCore.Mvc;
using KundeEntity = ERP.Data.Entitaeten.Kunde;

namespace ERP.Api.Controllers
{
    [ApiController]
    [Route("api/kunden")]
    public class KundenController : ControllerBase
    {
        private readonly KundeRepository _repository;

        public KundenController(KundeRepository kundeRepository)
        {
            _repository = kundeRepository;
        }

        // GET api/kunden
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.AlleKunden());
        }

        // GET api/kunden/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var kunde = _repository.FindById(id);
            if (kunde == null)
                return NotFound();
            return Ok(kunde);
        }

        // POST api/kunden
        [HttpPost]
        public IActionResult Post([FromBody] KundeEntity kunde)
        {
            if (kunde == null)
                return BadRequest("Kundendaten fehlen im Request-Body.");
            if (string.IsNullOrWhiteSpace(kunde.Name))
                return BadRequest("Name ist erforderlich.");
            if (string.IsNullOrWhiteSpace(kunde.Anschrift))
                return BadRequest("Anschrift ist erforderlich.");

            _repository.Hinzufuegen(kunde);
            return Created($"api/kunden/{kunde.Id}", kunde);
        }
    }
}
