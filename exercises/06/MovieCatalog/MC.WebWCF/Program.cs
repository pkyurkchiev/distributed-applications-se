using MC.ApplicationServices.Implementations;
using MC.ApplicationServices.Interfaces;
using MC.Data.Contexts;
using MC.Repositories.Implementations;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

try
{
    Log.Logger.Information("Application is starting!");

    var builder = WebApplication.CreateBuilder();

    // Add services to the container.
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<MovieCatalogDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddServiceModelServices();
    builder.Services.AddServiceModelMetadata();
    builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

    // Add serilog
    builder.Services.AddSerilog();

    // Start SERVICE DI
    builder.Services.AddScoped<DbContext, MovieCatalogDbContext>();
    builder.Services.AddScoped<IGenresRepository, GenresRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IGenresManagementService, GenresManagementService>();
    builder.Services.AddTransient<GenresService>();
    // End SERVICE DI

    var app = builder.Build();

    app.UseServiceModel(serviceBuilder =>
    {
        serviceBuilder.AddService<Service>();
        serviceBuilder.AddServiceEndpoint<Service, IService>(new BasicHttpBinding(), "/Service.svc");
        serviceBuilder.AddService<GenresService>();
        serviceBuilder.AddServiceEndpoint<GenresService, IGenresService>(new BasicHttpBinding(), "/GenresService.svc");
        var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
        serviceMetadataBehavior.HttpGetEnabled = true;
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception!");
}
finally
{
    await Log.CloseAndFlushAsync();
}