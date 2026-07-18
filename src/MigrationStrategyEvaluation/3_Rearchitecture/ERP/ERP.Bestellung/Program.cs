
using ERP.Bestellung.Repositories;
using ERP.Bestellung.Services;
using ERP.Data;

namespace ERP.Bestellung;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.

        builder.Services.AddControllers();

        builder.AddSqlServerDbContext<ErpContext>("erp");
        builder.Services.AddScoped<BestellungRepository>();
        builder.Services.AddScoped<Bestellabwicklung>();

        builder.Services.AddHttpClient("erp-kunde", client =>
            client.BaseAddress = new Uri("https+http://erp-kunde"));
        builder.Services.AddHttpClient("erp-artikel", client =>
            client.BaseAddress = new Uri("https+http://erp-artikel"));
        builder.Services.AddHttpClient("erp-lagerstand", client =>
            client.BaseAddress = new Uri("https+http://erp-lagerstand"));

        var app = builder.Build();

        app.MapDefaultEndpoints();


        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
