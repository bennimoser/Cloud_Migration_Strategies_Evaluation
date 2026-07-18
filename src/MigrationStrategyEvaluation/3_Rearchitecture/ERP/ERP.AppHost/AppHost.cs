var builder = DistributedApplication.CreateBuilder(args);

var kunde = builder.AddProject<Projects.ERP_Kunde>("erp-kunde");

var artikel = builder.AddProject<Projects.ERP_Artikel>("erp-artikel");

var lagerstand = builder.AddProject<Projects.ERP_Lagerstand>("erp-lagerstand");

var bestellung = builder.AddProject<Projects.ERP_Bestellung>("erp-bestellung")
    .WithReference(kunde)
    .WithReference(artikel)
    .WithReference(lagerstand);

builder.AddYarp("erp-gateway")
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/api/lager/{**catch-all}", lagerstand);
        yarp.AddRoute("/api/kunde/{**catch-all}", kunde);
        yarp.AddRoute("/api/artikel/{**catch-all}", artikel);
        yarp.AddRoute("/api/bestellung/{**catch-all}", bestellung);
    });

builder.Build().Run();
