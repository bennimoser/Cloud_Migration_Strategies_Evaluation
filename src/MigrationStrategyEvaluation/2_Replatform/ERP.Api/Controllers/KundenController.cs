using System.Web.Http;
using ERP.Data;
using ERP.Data.Repositories;
using ERP.Data.Entitaeten;

namespace ERP.Api.Controllers
{
    public class KundenController : ApiController
    {
        private readonly ErpContext _context;
        private readonly KundeRepository _repository;

        public KundenController()
        {
            _context = new ErpContext();
            _repository = new KundeRepository(_context);
        }

        // GET api/kunden
        public IHttpActionResult Get()
        {
            return Ok(_repository.AlleKunden());
        }

        // GET api/kunden/5
        public IHttpActionResult Get(int id)
        {
            var kunde = _repository.FindById(id);
            if (kunde == null)
                return NotFound();
            return Ok(kunde);
        }

        // POST api/kunden
        public IHttpActionResult Post([FromBody] Kunde kunde)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
