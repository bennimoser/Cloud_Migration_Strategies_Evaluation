using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<AzureSqlServerResource> sql;

if (builder.ExecutionContext.IsPublishMode)
{
    var existingName = builder.AddParameter("existingSqlServerName");
    var existingResourceGroup = builder.AddParameter("existingSqlServerResourceGroup");

    sql = builder.AddAzureSqlServer("sql")
        .AsExisting(existingName, existingResourceGroup);

}
else
{
    var password = builder.AddParameter("sqlPassword", secret: true);

    sql = builder.AddAzureSqlServer("sql")
        .RunAsContainer(options =>
        {
            options.WithLifetime(ContainerLifetime.Persistent);
            options.WithHostPort(1433);
            options.WithContainerName("sql");
            options.WithPassword(password);
        });
}

var erp = sql.AddDatabase("erp");

var kunde = builder.AddProject<Projects.ERP_Kunde>("erp-kunde")
    .WithReference(erp)
    .WaitFor(erp);

var artikel = builder.AddProject<Projects.ERP_Artikel>("erp-artikel")
    .WithReference(erp)
    .WaitFor(erp);

var lagerstand = builder.AddProject<Projects.ERP_Lagerbestand>("erp-lagerbestand")
    .WithReference(erp)
    .WaitFor(erp);

var bestellung = builder.AddProject<Projects.ERP_Bestellung>("erp-bestellung")
    .WithReference(erp)
    .WaitFor(erp)
    .WithReference(kunde)
    .WithReference(artikel)
    .WithReference(lagerstand);

builder.AddYarp("erp-gateway")
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/api/lagerbestand/{**catch-all}", lagerstand);
        yarp.AddRoute("/api/kunden/{**catch-all}", kunde);
        yarp.AddRoute("/api/artikel/{**catch-all}", artikel);
        yarp.AddRoute("/api/bestellungen/{**catch-all}", bestellung);
    });

builder.Build().Run();
