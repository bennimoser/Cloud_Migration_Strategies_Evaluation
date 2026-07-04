# Cloud-Migrationsstrategien einer .NET-Framework-Applikation

Prototypen und Testartefakte zur Masterarbeit **„Von On-Premise in die Cloud: Evaluierung von Migrationsstrategien einer .NET-Framework-Applikation"**.

Dieses Repository enthält eine Solution mit **vier Prototypen**, sodass die gesamte Untersuchung abgedeckt ist: die **Ausgangssituation** sowie die drei daraus abgeleiteten Migrationsstrategien **Rehost**, **Replatform** und **Rearchitecture** auf Basis von Microsoft Azure.

---

## Kontext der Arbeit

| | |
|---|---|
| **Autor** | Benjamin Moser BSc |
| **Studiengang** | Cloud Computing Engineering (MSc), Hochschule Burgenland |
| **Betreuer** | Ing. Igor Ivkić, BSc MSc |
| **Cloud-Plattform** | Microsoft Azure |

Die Arbeit vergleicht drei der sogenannten **7Rs** empirisch anhand der Performance einer REST-API. Als Ausgangspunkt dient ein eigenentwickeltes, on-premises betriebenes **ERP-System einer kleinen Handelsfirma** auf Basis des **.NET Framework (Legacy)**.

### Funktionsumfang des ERP-Systems (Ausgangssituation)

- Bestellabwicklung
- Lagerverwaltung
- Artikelverwaltung
- Kundenverwaltung

### Bewusste Scope-Einschränkungen

- **Kein Authentifizierungsmechanismus** – erhöhter Entwicklungsaufwand ohne Einfluss auf die erhobenen Performance-Metriken.
- **Keine Benutzeroberfläche** – der Vergleich fokussiert ausschließlich auf REST-Schnittstellen; eine UI-Migration ist nicht Gegenstand der Arbeit.

---

## Aufbau der Solution

Die Solution enthält vier Prototypen:

| Prototyp | Rolle | Service-Modell | Zieltechnologie |
|----------|-------|----------------|-----------------|
| **Ausgangssituation** | Referenz, on-premises Ausgangszustand | On-Premise | .NET-Framework-Applikation (Legacy) |
| **Rehost** (Lift-and-Shift) | Migrationsstrategie 1 | IaaS | Unveränderter Umzug, z. B. auf eine Azure VM |
| **Replatform** (Lift-and-Reshape) | Migrationsstrategie 2 | PaaS | Gezielte Anpassungen ohne Architektur-/Quellcodeumbau, z. B. Azure App Service + Azure SQL-Datenbank |
| **Rearchitecture** | Migrationsstrategie 3 | Cloud-Native | Grundlegender Architekturumbau mit **.NET Aspire**; Deployment als Docker-Container via **Azure Developer CLI (azd) + generierte Bicep-Skripte** |

> Hintergrund: Mit steigendem Migrationsaufwand steigt tendenziell auch der Grad der Cloud-Nativität (Rehost = geringster Aufwand/Cloud-Nativität, Rearchitecture = höchster Aufwand/Cloud-Nativität).

Für den Rearchitecture-Prototyp beschreibt das **AppHost**-Projekt die Orchestrierung der Systemkomponenten, das **ServiceDefaults**-Projekt verteilt die zentrale Konfiguration (u. a. Telemetrie-Endpunkte, Health Checks). .NET-Projekte laufen unter .NET Aspire lokal nicht containerisiert; beim Deployment via `azd` werden sie in der Cloud als Docker-Container ausgeführt.

---

## Performance-Tests & Auswertung

Die Prototypen der drei Migrationsstrategien werden nach einem festgelegten Testablauf getestet; die Auswertung erfolgt anhand des **Goal-Question-Metric (GQM)**-Ansatzes.

**Erhobene Metriken:**

| Metrik | Beschreibung |
|--------|--------------|
| M1.1 / M1.2 | Median bzw. 95. Perzentil (p95) der Antwortzeit |
| M2.1 | Rate fehlgeschlagener Anfragen |
| M3.1 | Durchsatz (Anfragen pro Sekunde) |
| M4.1 / M4.2 | Maximaler stabiler Durchsatz bis zur Degradierung / erste degradierende Laststufe |
| M5.1 / M5.2 | Laufende Kosten pro Monat / Kosten pro 1.000 Requests |

Das eingesetzte Test-Tool ist noch nicht abschließend festgelegt. In der herangezogenen Literatur am häufigsten verwendet: **k6**, **Apache JMeter** und **Postman**.
