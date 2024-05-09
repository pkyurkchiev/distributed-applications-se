using MC.ApplicationServices.Implementations;
using MC.ApplicationServices.Interfaces;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Serilog;
using Microsoft.OpenApi.Models;
using MC.Repositories.Implementations;
using MC.Repositories.Interfaces;

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

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<MovieCatalogDbContext>(options => options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("MC.WebAPI")));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "Movies catalog",
            Description = "RESTful Api for project Movie data catalog!",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            },
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
    // Add serilog
    builder.Services.AddSerilog();

    // Start SERVICE DI
    builder.Services.AddScoped<DbContext, MovieCatalogDbContext>();
    builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IMoviesManagementService, MoviesManagementService>();
    builder.Services.AddScoped<IGenresManagementService, GenresManagementService>();
    // End SERVICE DI

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

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