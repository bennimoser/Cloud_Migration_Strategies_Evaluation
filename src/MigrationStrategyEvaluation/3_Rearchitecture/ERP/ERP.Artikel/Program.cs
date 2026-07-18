
using ERP.Artikel.Repositories;

namespace ERP.Artikel;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<ArtikelRepository>();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
