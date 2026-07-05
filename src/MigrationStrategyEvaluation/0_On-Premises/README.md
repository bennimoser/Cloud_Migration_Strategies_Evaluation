# ERP On-Premises Baseline

On-Premises-Referenzimplementierung des ERP-Systems als Ausgangspunkt für den Cloud-Migrationsvergleich.

| | |
|---|---|
| **Framework** | .NET Framework 4.6.2 |
| **REST** | ASP.NET Web API 2 (5.2.9) |
| **ORM** | Entity Framework 6 (6.4.4), Code-First |
| **Datenbank** | SQL Server LocalDB (MSSQLLocalDB) |
| **Hosting** | IIS Express |
| **JSON** | Newtonsoft.Json 13.0.3 |

---

## Projektstruktur

```
0_On-Premises/
├── ERP.Domain/          # Domänenentitäten (Artikel, Kunde, Bestellung, …)
├── ERP.Data/            # EF6-DbContext, Repositories, Seed-Daten
└── ERP.Api/             # ASP.NET Web API 2 – Controller, Bestellabwicklung-Service
```

---

## Voraussetzungen

- **Visual Studio 2017** oder neuer (Community/Professional/Enterprise)
  - Workload „ASP.NET und Webentwicklung" installiert
- **.NET Framework 4.6.2 Developer Pack**
- **SQL Server Express LocalDB** (im Visual-Studio-Installer enthalten)
- **NuGet** (ab 4.x, in VS 2017+ integriert)

LocalDB-Instanz prüfen:
```cmd
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

---

## Build & Run

### 1. Lösung öffnen

```
src/MigrationStrategyEvaluation/MigrationStrategyEvaluation.slnx
```

> VS 2022 17.10+ öffnet `.slnx` direkt. Bei älteren Versionen:  
> Rechtsklick → „Öffnen mit" → Visual Studio.

### 2. NuGet-Pakete wiederherstellen

Visual Studio stellt Pakete beim ersten Build automatisch wieder her.  
Alternativ per CLI (im Solution-Verzeichnis):

```cmd
nuget restore MigrationStrategyEvaluation.slnx
```

### 3. Startprojekt setzen

Rechtsklick auf **ERP.Api** → „Als Startprojekt festlegen".

### 4. Starten

`F5` oder **Debug → Starten mit Debugging**.

IIS Express startet; die Basis-URL lautet z. B. `http://localhost:64537`  
(Port wird automatisch vergeben und im Browserfenster angezeigt).

### Datenbankinitialisierung

Beim ersten Start legt EF6 automatisch die Datenbank **ERP_OnPremises** in der  
LocalDB-Instanz an und befüllt sie mit:
- **75 Artikel** (Bürobedarf, Elektronik, Möbel, Werkzeug, Reinigung/Sicherheit)
- **entsprechende Lagerbestände**
- **20 Kunden** (deutsche Handelsfirmen)

---

## REST-Endpunkte

Basis-URL: `http://localhost:{PORT}`

### Artikel

| Methode | URL | Beschreibung |
|---------|-----|--------------|
| GET | `/api/artikel` | Alle Artikel |
| GET | `/api/artikel/{id}` | Artikel nach ID |
| POST | `/api/artikel` | Neuen Artikel anlegen |
| PUT | `/api/artikel/{id}` | Artikel aktualisieren |

### Kunden

| Methode | URL | Beschreibung |
|---------|-----|--------------|
| GET | `/api/kunden` | Alle Kunden |
| GET | `/api/kunden/{id}` | Kunde nach ID |
| POST | `/api/kunden` | Neuen Kunden anlegen |

### Lagerbestand

| Methode | URL | Beschreibung |
|---------|-----|--------------|
| GET | `/api/lagerbestand/{artikelId}` | Lagerbestand für einen Artikel |
| PUT | `/api/lagerbestand/{artikelId}` | Lagerbestand aktualisieren |

### Bestellungen

| Methode | URL | Beschreibung |
|---------|-----|--------------|
| GET | `/api/bestellungen` | Alle Bestellungen |
| GET | `/api/bestellungen/{id}` | Bestellung nach ID |
| POST | `/api/bestellungen` | Neue Bestellung aufgeben |

---

## Beispiel-Requests (curl)

> Port anpassen – IIS Express vergibt ihn dynamisch.

```bash
PORT=64537

# Alle Artikel abrufen
curl -X GET http://localhost:$PORT/api/artikel

# Einzelnen Artikel abrufen
curl -X GET http://localhost:$PORT/api/artikel/1

# Neuen Artikel anlegen
curl -X POST http://localhost:$PORT/api/artikel \
  -H "Content-Type: application/json" \
  -d '{"artikelnummer":"A0099","bezeichnung":"Testprodukt","verkaufspreis":19.99}'

# Artikel aktualisieren
curl -X PUT http://localhost:$PORT/api/artikel/1 \
  -H "Content-Type: application/json" \
  -d '{"artikelnummer":"A0001","bezeichnung":"Kugelschreiber blau, 10er Pack – neu","verkaufspreis":4.49}'

# Alle Kunden abrufen
curl -X GET http://localhost:$PORT/api/kunden

# Neuen Kunden anlegen
curl -X POST http://localhost:$PORT/api/kunden \
  -H "Content-Type: application/json" \
  -d '{"name":"Testfirma GmbH","anschrift":"Musterstraße 1, 12345 Musterstadt","kontaktdaten":"info@testfirma.de"}'

# Lagerbestand für Artikel 1 abrufen
curl -X GET http://localhost:$PORT/api/lagerbestand/1

# Lagerbestand aktualisieren
curl -X PUT http://localhost:$PORT/api/lagerbestand/1 \
  -H "Content-Type: application/json" \
  -d '{"menge":500,"mindestbestand":50}'

# Alle Bestellungen abrufen
curl -X GET http://localhost:$PORT/api/bestellungen

# Bestellung aufgeben (KundeId=1, Artikel 1 × 3 Stk., Artikel 2 × 10 Stk.)
curl -X POST http://localhost:$PORT/api/bestellungen \
  -H "Content-Type: application/json" \
  -d '{
    "kundeId": 1,
    "positionen": [
      { "artikelId": 1, "menge": 3 },
      { "artikelId": 2, "menge": 10 }
    ]
  }'
```

---

## HTTP-Statuscodes

| Code | Bedeutung |
|------|-----------|
| 200 OK | Anfrage erfolgreich |
| 201 Created | Ressource angelegt (inkl. `Location`-Header) |
| 400 Bad Request | Validierungsfehler, unzureichender Lagerbestand |
| 404 Not Found | Ressource nicht gefunden |

---

## Bestellabwicklung – Ablauf

1. Prüfen ob der angegebene Kunde existiert → 404 bei unbekanntem Kunden
2. Für jede Position: Artikel und Lagerbestand prüfen → 400 bei fehlendem Artikel oder unzureichendem Bestand
3. Erst nach **allen** erfolgreichen Prüfungen die Bestellung anlegen
4. Lagerbestand für jede Position reduzieren
5. Alles in **einer** `SaveChanges()`-Transaktion (implizit via EF6) → atomarer Rollback

---

## MSBuild (ohne Visual Studio)

```cmd
msbuild 0_On-Premises\ERP.Api\ERP.Api.csproj /p:Configuration=Release /restore
```

Deployment-Paket für IIS erstellen:

```cmd
msbuild 0_On-Premises\ERP.Api\ERP.Api.csproj \
  /p:Configuration=Release \
  /p:DeployOnBuild=true \
  /p:WebPublishMethod=Package \
  /p:PackageLocation=.\publish
```
