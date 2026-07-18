using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Api.Models;
using ERP.Data;
using ERP.Data.Entitaeten;

namespace ERP.Api.Services
{
    public class Bestellabwicklung
    {
        private readonly ErpContext _context;

        public Bestellabwicklung(ErpContext context)
        {
            _context = context;
        }

        public int BestellungAufgeben(BestellungAnfrageDto anfrage)
        {
            if (anfrage == null || anfrage.Positionen == null || anfrage.Positionen.Count == 0)
                throw new ArgumentException("Anfrage enthält keine Bestellpositionen.");

            // 1. Kunde prüfen
            var kunde = _context.Kunden.Find(anfrage.KundeId);
            if (kunde == null)
                throw new KeyNotFoundException($"Kunde mit ID {anfrage.KundeId} wurde nicht gefunden.");

            // 2. Alle Artikel und Lagerbestände vorab prüfen (vor dem Anlegen der Bestellung)
            var pruefListe = anfrage.Positionen.Select(p => new
            {
                Anfrage = p,
                Artikel = _context.Artikel.Find(p.ArtikelId),
                Lagerbestand = _context.Lagerbestaende.FirstOrDefault(l => l.ArtikelId == p.ArtikelId)
            }).ToList();

            foreach (var pos in pruefListe)
            {
                if (pos.Artikel == null)
                    throw new KeyNotFoundException($"Artikel mit ID {pos.Anfrage.ArtikelId} wurde nicht gefunden.");

                if (pos.Lagerbestand == null)
                    throw new InvalidOperationException(
                        $"Kein Lagerbestand für Artikel '{pos.Artikel.Bezeichnung}' (ID {pos.Artikel.Id}) vorhanden.");

                if (pos.Lagerbestand.Menge < pos.Anfrage.Menge)
                    throw new InvalidOperationException(
                        $"Lagerbestand für Artikel '{pos.Artikel.Bezeichnung}' nicht ausreichend. " +
                        $"Verfügbar: {pos.Lagerbestand.Menge}, Angefordert: {pos.Anfrage.Menge}.");
            }

            // 3. Bestellung anlegen (erst nach ALLEN erfolgreichen Prüfungen)
            var bestellung = new Bestellung
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

            // 4. Lagerbestände reduzieren
            foreach (var pos in pruefListe)
            {
                pos.Lagerbestand.Menge -= pos.Anfrage.Menge;
            }

            _context.Bestellungen.Add(bestellung);

            // Implizite EF6-Transaktion: alle Änderungen atomar oder gar nicht
            _context.SaveChanges();

            return bestellung.Id;
        }
    }
}
