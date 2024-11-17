using Links.Services;
using Pages.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<List<RedirectItem>>(builder.Configuration.GetSection("Links"));
builder.Services.AddScoped(typeof(GetLinkService));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
   .AllowAnyHeader()
   .AllowAnyMethod()
   .SetIsOriginAllowed(host => true)
   .AllowCredentials());

app.MapControllers();

app.Run();
