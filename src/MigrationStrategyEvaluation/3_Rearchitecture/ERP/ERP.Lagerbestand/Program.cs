
using ERP.Data;
using ERP.Lagerstand.Repositories;

namespace ERP.Lagerstand;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.

        builder.Services.AddControllers();

        builder.AddSqlServerDbContext<ErpContext>("erp");
        builder.Services.AddScoped<LagerbestandRepository>();

        var app = builder.Build();

        app.MapDefaultEndpoints();


        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
