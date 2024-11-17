using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Links.Data;
using Links.Data.Migrations;
using Links.Repositories;
using Links.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MariaDB");

builder.Services.AddDbContext<LinksDbContext>(conf =>
{
    var serverVersion = ServerVersion.AutoDetect(connectionString);

    conf.UseMySql(connectionString, serverVersion);
    conf.EnableSensitiveDataLogging();
    conf.EnableDetailedErrors();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(GetLinkService));
builder.Services.AddScoped(typeof(RedirectItemRepository));
builder.Services.AddScoped(typeof(CacheService));
builder.Services.AddMemoryCache();

// FluentMigrator
builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(cfg => cfg
        .AddMySql5()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(CreateTabelaVersionInfoMetadata).Assembly)
        .For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .AddTransient<IVersionTableMetaData, CreateTabelaVersionInfoMetadata>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
   .AllowAnyHeader()
   .AllowAnyMethod()
   .SetIsOriginAllowed(host => true)
   .AllowCredentials());

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
