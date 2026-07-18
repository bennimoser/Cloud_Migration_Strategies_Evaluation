using System.Net.Http.Json;
using ERP.Bestellung.Models;
using ERP.Data;
using ERP.Data.Entitaeten;
using BestellungEntity = ERP.Data.Entitaeten.Bestellung;

namespace ERP.Bestellung.Services
{
    public class Bestellabwicklung
    {
        private readonly ErpContext _context;
        private readonly HttpClient _kundeClient;
        private readonly HttpClient _artikelClient;
        private readonly HttpClient _lagerstandClient;

        public Bestellabwicklung(ErpContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _kundeClient = httpClientFactory.CreateClient("erp-kunde");
            _artikelClient = httpClientFactory.CreateClient("erp-artikel");
            _lagerstandClient = httpClientFactory.CreateClient("erp-lagerstand");
        }

        public async Task<int> BestellungAufgeben(BestellungAnfrageDto anfrage)
        {
            if (anfrage == null || anfrage.Positionen == null || anfrage.Positionen.Count == 0)
                throw new ArgumentException("Anfrage enthält keine Bestellpositionen.");

            // 1. Kunde prüfen via ERP.Kunde
            var kundeResponse = await _kundeClient.GetAsync($"api/kunden/{anfrage.KundeId}");
            if (!kundeResponse.IsSuccessStatusCode)
                throw new KeyNotFoundException($"Kunde mit ID {anfrage.KundeId} wurde nicht gefunden.");

            // 2. Alle Artikel und Lagerbestände vorab prüfen via ERP.Artikel + ERP.Lagerstand
            var pruefListe = new List<PruefPosition>();
            foreach (var p in anfrage.Positionen)
            {
                var artikelResponse = await _artikelClient.GetAsync($"api/artikel/{p.ArtikelId}");
                if (!artikelResponse.IsSuccessStatusCode)
                    throw new KeyNotFoundException($"Artikel mit ID {p.ArtikelId} wurde nicht gefunden.");
                var artikel = await artikelResponse.Content.ReadFromJsonAsync<ArtikelAntwort>()
                    ?? throw new KeyNotFoundException($"Artikel mit ID {p.ArtikelId} wurde nicht gefunden.");

                var lagerResponse = await _lagerstandClient.GetAsync($"api/lagerbestand/{p.ArtikelId}");
                if (!lagerResponse.IsSuccessStatusCode)
                    throw new InvalidOperationException(
                        $"Kein Lagerbestand für Artikel '{artikel.Bezeichnung}' (ID {p.ArtikelId}) vorhanden.");
                var lager = await lagerResponse.Content.ReadFromJsonAsync<LagerbestandAntwort>()
                    ?? throw new InvalidOperationException(
                        $"Kein Lagerbestand für Artikel '{artikel.Bezeichnung}' (ID {p.ArtikelId}) vorhanden.");

                if (lager.Menge < p.Menge)
                    throw new InvalidOperationException(
                        $"Lagerbestand für Artikel '{artikel.Bezeichnung}' nicht ausreichend. " +
                        $"Verfügbar: {lager.Menge}, Angefordert: {p.Menge}.");

                pruefListe.Add(new PruefPosition(p, artikel, lager));
            }

            // 3. Bestellung anlegen (erst nach ALLEN erfolgreichen Prüfungen)
            var bestellung = new BestellungEntity
            {
                Bestelldatum = DateTime.UtcNow,
                KundeId = anfrage.KundeId,
                Positionen = pruefListe.Select(pos => new Bestellposition
                {
                    ArtikelId = pos.Anfrage.ArtikelId,
                    Menge = pos.Anfrage.Menge,
                    Einzelpreis = pos.Artikel.Verkaufspreis
                }).ToList()
            };

            _context.Bestellungen.Add(bestellung);
            _context.SaveChanges();

            // 4. Lagerbestände reduzieren via ERP.Lagerstand (nach erfolgreich angelegter Bestellung)
            foreach (var pos in pruefListe)
            {
                var neuerBestand = new { Menge = pos.Lager.Menge - pos.Anfrage.Menge, pos.Lager.Mindestbestand };
                await _lagerstandClient.PutAsJsonAsync($"api/lagerbestand/{pos.Anfrage.ArtikelId}", neuerBestand);
            }

            return bestellung.Id;
        }
    }

    file record ArtikelAntwort(int Id, string Bezeichnung, decimal Verkaufspreis);
    file record LagerbestandAntwort(int Id, int ArtikelId, int Menge, int Mindestbestand);
    file record PruefPosition(BestellpositionAnfrageDto Anfrage, ArtikelAntwort Artikel, LagerbestandAntwort Lager);
}
