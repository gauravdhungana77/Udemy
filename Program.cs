using Hotel_Listings.Configuration;
using Hotel_Listings.Data;
using Hotel_Listings.IRepository;
using Hotel_Listings.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = new LoggerConfiguration()
// Read from appsettings.json
    .ReadFrom.Configuration(builder.Configuration, sectionName: "Serilog")
    .Enrich.FromLogContext()
    // Create the actual logger
    .CreateLogger();
    logger.Information("Application Starting");

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
Log.CloseAndFlush();

//Setting up cors policy

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsePolicy", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

//Creating sql connection

builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

//Avoiding circular dependency


//Dependency injection
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MapperInitializer));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
