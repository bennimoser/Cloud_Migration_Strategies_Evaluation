using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ERP.Data.Entitaeten;

namespace ERP.Data.Seed
{
    public static class ErpSeedDaten
    {
        public static void Initialisieren(ErpContext context)
        {
            SeedArtikel(context);
            context.SaveChanges();
            SeedLagerbestaende(context);
            context.SaveChanges();
            SeedKunden(context);
            context.SaveChanges();
        }

        private static void SeedArtikel(ErpContext context)
        {
            var artikel = new List<Artikel>
            {
                // Bürobedarf (A0001–A0020)
                new Artikel { Artikelnummer = "A0001", Bezeichnung = "Kugelschreiber blau, 10er Pack", Verkaufspreis = 3.99m },
                new Artikel { Artikelnummer = "A0002", Bezeichnung = "Bleistift HB, 12er Pack", Verkaufspreis = 2.49m },
                new Artikel { Artikelnummer = "A0003", Bezeichnung = "DIN A4 Kopierpapier, 500 Blatt", Verkaufspreis = 4.99m },
                new Artikel { Artikelnummer = "A0004", Bezeichnung = "Tacker mit 1000 Heftklammern", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0005", Bezeichnung = "Ordner A4 breit, blau", Verkaufspreis = 4.49m },
                new Artikel { Artikelnummer = "A0006", Bezeichnung = "Ordner A4 schmal, grün", Verkaufspreis = 3.99m },
                new Artikel { Artikelnummer = "A0007", Bezeichnung = "Locher 2-fach, Metall", Verkaufspreis = 8.99m },
                new Artikel { Artikelnummer = "A0008", Bezeichnung = "Büroschere 21cm", Verkaufspreis = 5.49m },
                new Artikel { Artikelnummer = "A0009", Bezeichnung = "Briefumschläge C5, 50er Pack", Verkaufspreis = 6.99m },
                new Artikel { Artikelnummer = "A0010", Bezeichnung = "Post-it Notes 76x76mm, 12er Pack", Verkaufspreis = 9.99m },
                new Artikel { Artikelnummer = "A0011", Bezeichnung = "Textmarker Set, 6 Farben", Verkaufspreis = 7.99m },
                new Artikel { Artikelnummer = "A0012", Bezeichnung = "Permanentmarker schwarz, 5er Pack", Verkaufspreis = 8.49m },
                new Artikel { Artikelnummer = "A0013", Bezeichnung = "Klebefilm transparent, 10er Pack", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0014", Bezeichnung = "Doppelklebeband 5er Pack", Verkaufspreis = 14.99m },
                new Artikel { Artikelnummer = "A0015", Bezeichnung = "Büroklammern, 100er Schachtel", Verkaufspreis = 1.99m },
                new Artikel { Artikelnummer = "A0016", Bezeichnung = "Gummibänder, 500g Beutel", Verkaufspreis = 4.99m },
                new Artikel { Artikelnummer = "A0017", Bezeichnung = "Stempelkissen blau", Verkaufspreis = 6.49m },
                new Artikel { Artikelnummer = "A0018", Bezeichnung = "Namensschilder selbstklebend, 100er Pack", Verkaufspreis = 8.99m },
                new Artikel { Artikelnummer = "A0019", Bezeichnung = "Whiteboard-Marker Set, 4 Farben", Verkaufspreis = 9.99m },
                new Artikel { Artikelnummer = "A0020", Bezeichnung = "Haftnotizblock 100 Blatt, gelb", Verkaufspreis = 3.49m },
                // Elektronik (A0021–A0040)
                new Artikel { Artikelnummer = "A0021", Bezeichnung = "USB-Hub 4-Port USB 3.0", Verkaufspreis = 19.99m },
                new Artikel { Artikelnummer = "A0022", Bezeichnung = "HDMI Kabel 1,5m", Verkaufspreis = 9.99m },
                new Artikel { Artikelnummer = "A0023", Bezeichnung = "Netzwerkstecker RJ45 Cat6, 20er Pack", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0024", Bezeichnung = "Tastatur kabellos, DE-Layout", Verkaufspreis = 34.99m },
                new Artikel { Artikelnummer = "A0025", Bezeichnung = "Maus kabellos, ergonomisch", Verkaufspreis = 29.99m },
                new Artikel { Artikelnummer = "A0026", Bezeichnung = "Webcam Full HD 1080p", Verkaufspreis = 59.99m },
                new Artikel { Artikelnummer = "A0027", Bezeichnung = "Universal-Netzteil 65W", Verkaufspreis = 39.99m },
                new Artikel { Artikelnummer = "A0028", Bezeichnung = "USB-C Ladekabel 2m", Verkaufspreis = 14.99m },
                new Artikel { Artikelnummer = "A0029", Bezeichnung = "Verlängerungskabel 5m, 3-fach", Verkaufspreis = 22.99m },
                new Artikel { Artikelnummer = "A0030", Bezeichnung = "Überspannungsschutz 5-fach", Verkaufspreis = 34.99m },
                new Artikel { Artikelnummer = "A0031", Bezeichnung = "Laptop-Kühler USB", Verkaufspreis = 24.99m },
                new Artikel { Artikelnummer = "A0032", Bezeichnung = "Externe SSD 500GB USB 3.0", Verkaufspreis = 69.99m },
                new Artikel { Artikelnummer = "A0033", Bezeichnung = "Drucker-Tinte schwarz (XL)", Verkaufspreis = 24.99m },
                new Artikel { Artikelnummer = "A0034", Bezeichnung = "Drucker-Tinte farbig (XL)", Verkaufspreis = 29.99m },
                new Artikel { Artikelnummer = "A0035", Bezeichnung = "Toner-Kartusche schwarz", Verkaufspreis = 49.99m },
                new Artikel { Artikelnummer = "A0036", Bezeichnung = "Headset USB mit Mikrofon", Verkaufspreis = 44.99m },
                new Artikel { Artikelnummer = "A0037", Bezeichnung = "Bildschirm-Reinigungsset", Verkaufspreis = 9.99m },
                new Artikel { Artikelnummer = "A0038", Bezeichnung = "USB-Stick 64GB", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0039", Bezeichnung = "Netzwerkkabel Cat6, 10m", Verkaufspreis = 15.99m },
                new Artikel { Artikelnummer = "A0040", Bezeichnung = "Powerbank 10.000 mAh", Verkaufspreis = 29.99m },
                // Möbel & Einrichtung (A0041–A0055)
                new Artikel { Artikelnummer = "A0041", Bezeichnung = "Bürostuhl Standard, schwarz", Verkaufspreis = 249.99m },
                new Artikel { Artikelnummer = "A0042", Bezeichnung = "Schreibtisch 160x80cm, weiß", Verkaufspreis = 349.99m },
                new Artikel { Artikelnummer = "A0043", Bezeichnung = "Aktenschrank 2-türig, grau", Verkaufspreis = 299.99m },
                new Artikel { Artikelnummer = "A0044", Bezeichnung = "Hängemappenrahmen A4", Verkaufspreis = 29.99m },
                new Artikel { Artikelnummer = "A0045", Bezeichnung = "Dokumentenablage, 5 Fächer", Verkaufspreis = 49.99m },
                new Artikel { Artikelnummer = "A0046", Bezeichnung = "Whiteboard-Wischer Set, 5 Stück", Verkaufspreis = 14.99m },
                new Artikel { Artikelnummer = "A0047", Bezeichnung = "Magnetpinnwand 90x60cm", Verkaufspreis = 79.99m },
                new Artikel { Artikelnummer = "A0048", Bezeichnung = "Whiteboard 120x90cm", Verkaufspreis = 129.99m },
                new Artikel { Artikelnummer = "A0049", Bezeichnung = "Flipchart mit Papierblock", Verkaufspreis = 199.99m },
                new Artikel { Artikelnummer = "A0050", Bezeichnung = "Besprechungsstuhl, 4er Set", Verkaufspreis = 499.99m },
                new Artikel { Artikelnummer = "A0051", Bezeichnung = "Fußstütze ergonomisch, verstellbar", Verkaufspreis = 49.99m },
                new Artikel { Artikelnummer = "A0052", Bezeichnung = "Monitorständer, höhenverstellbar", Verkaufspreis = 39.99m },
                new Artikel { Artikelnummer = "A0053", Bezeichnung = "Schreibtischlampe LED, schwenkbar", Verkaufspreis = 59.99m },
                new Artikel { Artikelnummer = "A0054", Bezeichnung = "Papierkorb Metall, 15L", Verkaufspreis = 24.99m },
                new Artikel { Artikelnummer = "A0055", Bezeichnung = "Rollcontainer abschließbar, grau", Verkaufspreis = 449.99m },
                // Werkzeug (A0056–A0065)
                new Artikel { Artikelnummer = "A0056", Bezeichnung = "Schraubenzieher-Set, 12-teilig", Verkaufspreis = 24.99m },
                new Artikel { Artikelnummer = "A0057", Bezeichnung = "Kombizange 200mm", Verkaufspreis = 14.99m },
                new Artikel { Artikelnummer = "A0058", Bezeichnung = "Hammer 300g mit Holzstiel", Verkaufspreis = 19.99m },
                new Artikel { Artikelnummer = "A0059", Bezeichnung = "Maßband 5m, Metall", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0060", Bezeichnung = "Wasserwaage 60cm, Aluminium", Verkaufspreis = 22.99m },
                new Artikel { Artikelnummer = "A0061", Bezeichnung = "Akku-Schrauber 18V, inkl. 2 Akkus", Verkaufspreis = 89.99m },
                new Artikel { Artikelnummer = "A0062", Bezeichnung = "Bohrer-Bit-Set, 25-teilig", Verkaufspreis = 34.99m },
                new Artikel { Artikelnummer = "A0063", Bezeichnung = "Cutter-Messer, 3er Pack", Verkaufspreis = 8.99m },
                new Artikel { Artikelnummer = "A0064", Bezeichnung = "Doppelseitiges Klebeband extra stark, 10er", Verkaufspreis = 24.99m },
                new Artikel { Artikelnummer = "A0065", Bezeichnung = "Kabelbinder schwarz, 100er Pack", Verkaufspreis = 6.99m },
                // Reinigung & Sicherheit (A0066–A0075)
                new Artikel { Artikelnummer = "A0066", Bezeichnung = "Desinfektionsmittel Spray, 1L", Verkaufspreis = 9.99m },
                new Artikel { Artikelnummer = "A0067", Bezeichnung = "Bildschirm-Reinigungstücher, 100er", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0068", Bezeichnung = "Druckluft-Spray 400ml", Verkaufspreis = 8.99m },
                new Artikel { Artikelnummer = "A0069", Bezeichnung = "Nitril-Einweghandschuhe, 100er Gr. M", Verkaufspreis = 14.99m },
                new Artikel { Artikelnummer = "A0070", Bezeichnung = "Sicherheitsschuhe S1 Gr. 42", Verkaufspreis = 79.99m },
                new Artikel { Artikelnummer = "A0071", Bezeichnung = "Feuerlöscher CO2, 2kg", Verkaufspreis = 89.99m },
                new Artikel { Artikelnummer = "A0072", Bezeichnung = "Erste-Hilfe-Koffer Büro DIN 13157", Verkaufspreis = 34.99m },
                new Artikel { Artikelnummer = "A0073", Bezeichnung = "Schutzbrille klar, EN166", Verkaufspreis = 12.99m },
                new Artikel { Artikelnummer = "A0074", Bezeichnung = "Warnweste gelb Gr. M/L", Verkaufspreis = 8.99m },
                new Artikel { Artikelnummer = "A0075", Bezeichnung = "Gehörschutz-Stöpsel, 50 Paar", Verkaufspreis = 9.99m },
            };

            foreach (var a in artikel)
            {
                context.Artikel.AddOrUpdate(x => x.Artikelnummer, a);
            }
        }

        private static void SeedLagerbestaende(ErpContext context)
        {
            foreach (var artikel in context.Artikel.ToList())
            {
                if (context.Lagerbestaende.Any(l => l.ArtikelId == artikel.Id))
                    continue;

                int num = int.Parse(artikel.Artikelnummer.Substring(1));
                int menge, mindestbestand;

                if (num <= 20)      { menge = 200; mindestbestand = 20; }  // Bürobedarf
                else if (num <= 40) { menge = 50;  mindestbestand = 5; }   // Elektronik
                else if (num <= 55) { menge = 20;  mindestbestand = 3; }   // Möbel
                else if (num <= 65) { menge = 100; mindestbestand = 10; }  // Werkzeug
                else                { menge = 150; mindestbestand = 15; }  // Reinigung/Sicherheit

                context.Lagerbestaende.Add(new Entitaeten.Lagerbestand
                {
                    ArtikelId = artikel.Id,
                    Menge = menge,
                    Mindestbestand = mindestbestand
                });
            }
        }

        private static void SeedKunden(ErpContext context)
        {
            var kunden = new List<Entitaeten.Kunde>
            {
                new Entitaeten.Kunde { Name = "Müller Handels GmbH",              Anschrift = "Mariahilfer Straße 45, 1060 Wien",           Kontaktdaten = "info@mueller-handel.at" },
                new Entitaeten.Kunde { Name = "Becker Bürobedarf GmbH",           Anschrift = "Herrengasse 12, 8010 Graz",                  Kontaktdaten = "einkauf@becker-buero.at" },
                new Entitaeten.Kunde { Name = "Schmidt & Partner KG",             Anschrift = "Landstraße 30, 4020 Linz",                   Kontaktdaten = "bestellung@schmidt-partner.at" },
                new Entitaeten.Kunde { Name = "Weber Technik GmbH",               Anschrift = "Museumstraße 8, 6020 Innsbruck",             Kontaktdaten = "office@weber-technik.at" },
                new Entitaeten.Kunde { Name = "Fischer Elektronik GmbH",          Anschrift = "Alter Platz 1, 9020 Klagenfurt",             Kontaktdaten = "vertrieb@fischer-elektronik.at" },
                new Entitaeten.Kunde { Name = "Wagner Logistik AG",               Anschrift = "Ignaz-Harrer-Straße 20, 5020 Salzburg",      Kontaktdaten = "office@wagner-logistik.at" },
                new Entitaeten.Kunde { Name = "Gruber Schreibwaren KG",           Anschrift = "Hauptplatz 5, 9500 Villach",                 Kontaktdaten = "bestellung@gruber-schreibwaren.at" },
                new Entitaeten.Kunde { Name = "Hoffmann Bürotechnik GmbH",        Anschrift = "Bahnhofstraße 22, 6900 Bregenz",             Kontaktdaten = "einkauf@hoffmann-buero.at" },
                new Entitaeten.Kunde { Name = "Steiner Großhandel GmbH",          Anschrift = "Wiener Straße 15, 2700 Wiener Neustadt",     Kontaktdaten = "info@steiner-grosshandel.at" },
                new Entitaeten.Kunde { Name = "Huber Werkzeughandel GmbH",        Anschrift = "Domplatz 3, 5010 Salzburg",                  Kontaktdaten = "bestellung@huber-werkzeug.at" },
                new Entitaeten.Kunde { Name = "Pichler Medien GmbH",              Anschrift = "Opernring 7, 1010 Wien",                     Kontaktdaten = "info@pichler-medien.at" },
                new Entitaeten.Kunde { Name = "Bauer Handel & Service GmbH",      Anschrift = "Getreidegasse 18, 5020 Salzburg",            Kontaktdaten = "einkauf@bauer-handel.at" },
                new Entitaeten.Kunde { Name = "Maier Industriebedarf AG",         Anschrift = "Südtiroler Platz 1, 8020 Graz",              Kontaktdaten = "vertrieb@maier-industrie.at" },
                new Entitaeten.Kunde { Name = "Berger Technik GmbH",              Anschrift = "Bürgerstraße 10, 6020 Innsbruck",            Kontaktdaten = "info@berger-technik.at" },
                new Entitaeten.Kunde { Name = "Wolf Büroeinrichtungen GmbH",      Anschrift = "Praterstraße 44, 1020 Wien",                 Kontaktdaten = "office@wolf-buero.at" },
                new Entitaeten.Kunde { Name = "Hofer Bürokontor GmbH",            Anschrift = "Rathausplatz 2, 3100 St. Pölten",            Kontaktdaten = "einkauf@hofer-buerokontor.at" },
                new Entitaeten.Kunde { Name = "Winkler Handels KG",               Anschrift = "Stadtplatz 11, 4600 Wels",                   Kontaktdaten = "info@winkler-handel.at" },
                new Entitaeten.Kunde { Name = "Moser Officebedarf GmbH",          Anschrift = "Karmeliterplatz 4, 8010 Graz",               Kontaktdaten = "bestellung@moser-office.at" },
                new Entitaeten.Kunde { Name = "Eder Bürosysteme GmbH",            Anschrift = "Hafenstraße 2, 6800 Feldkirch",              Kontaktdaten = "vertrieb@eder-buero.at" },
                new Entitaeten.Kunde { Name = "Koller & Wallner GmbH",            Anschrift = "Ringstraße 25, 7000 Eisenstadt",             Kontaktdaten = "info@koller-wallner.at" },
            };

            foreach (var k in kunden)
            {
                context.Kunden.AddOrUpdate(x => x.Name, k);
            }
        }
    }
}
