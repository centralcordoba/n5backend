

using Microsoft.EntityFrameworkCore;
using n5.Application.Handlers;
using n5.Infrastructure;
using System.Reflection;
using MediatR;
using Serilog;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = new ConnectionSettings(new Uri("http://localhost:9200")) // Ajusta la URL según tu configuración
        .DefaultIndex("permission"); // El índice por defecto

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreatePermissionHandler).GetTypeInfo().Assembly));


builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .WriteTo.Console()
        .WriteTo.File("logs/myapp.txt")
);



builder.Services.AddDbContext<n5DbContext>(options =>
    options.UseInMemoryDatabase("PermissionDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
