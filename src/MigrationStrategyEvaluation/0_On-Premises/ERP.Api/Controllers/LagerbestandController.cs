using System.Web.Http;
using ERP.Data;
using ERP.Data.Repositories;

namespace ERP.Api.Controllers
{
    public class LagerbestandController : ApiController
    {
        private readonly ErpContext _context;
        private readonly LagerbestandRepository _repository;

        public LagerbestandController()
        {
            _context = new ErpContext();
            _repository = new LagerbestandRepository(_context);
        }

        // GET api/lagerbestand/5  (id = artikelId)
        public IHttpActionResult Get(int id)
        {
            var lagerbestand = _repository.FindByArtikelId(id);
            if (lagerbestand == null)
                return NotFound();
            return Ok(lagerbestand);
        }

        // PUT api/lagerbestand/5  (id = artikelId)
        public IHttpActionResult Put(int id, [FromBody] LagerbestandUpdateDto dto)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }

    public class LagerbestandUpdateDto
    {
        public int Menge { get; set; }
        public int Mindestbestand { get; set; }
    }
}
