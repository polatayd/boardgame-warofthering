using BoardGame.WarOfTheRing.Dice.Api.EndpointMappings;
using BoardGame.WarOfTheRing.Dice.Api.ServiceRegistrations;
using Serilog;
using Serilog.Formatting.Elasticsearch;

var logger = new LoggerConfiguration()
    .WriteTo.Console(new ElasticsearchJsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog(logger);

builder.Services.RegisterPoliticalTrackServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.RegisterDicePoolEndpoints();

app.Run();