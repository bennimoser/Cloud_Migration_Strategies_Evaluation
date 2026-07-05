using System;
using System.Collections.Generic;
using System.Web.Http;
using ERP.Api.Models;
using ERP.Api.Services;
using ERP.Data;
using ERP.Data.Repositories;

namespace ERP.Api.Controllers
{
    public class BestellungenController : ApiController
    {
        private readonly ErpContext _context;
        private readonly BestellungRepository _repository;

        public BestellungenController()
        {
            _context = new ErpContext();
            _repository = new BestellungRepository(_context);
        }

        // GET api/bestellungen
        public IHttpActionResult Get()
        {
            return Ok(_repository.AlleBestellungen());
        }

        // GET api/bestellungen/5
        public IHttpActionResult Get(int id)
        {
            var bestellung = _repository.FindById(id);
            if (bestellung == null)
                return NotFound();
            return Ok(bestellung);
        }

        // POST api/bestellungen
        public IHttpActionResult Post([FromBody] BestellungAnfrageDto anfrage)
        {
            if (anfrage == null)
                return BadRequest("Bestelldaten fehlen im Request-Body.");
            if (anfrage.Positionen == null || anfrage.Positionen.Count == 0)
                return BadRequest("Die Bestellung enthält keine Positionen.");

            var service = new Bestellabwicklung(_context);

            try
            {
                int bestellungId = service.BestellungAufgeben(anfrage);
                return Created($"api/bestellungen/{bestellungId}", new { id = bestellungId });
            }
            catch (KeyNotFoundException ex)
            {
                return Content(System.Net.HttpStatusCode.NotFound,
                    new { fehler = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Content(System.Net.HttpStatusCode.BadRequest,
                    new { fehler = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Content(System.Net.HttpStatusCode.BadRequest,
                    new { fehler = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
