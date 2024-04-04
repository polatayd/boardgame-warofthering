using BoardGame.WarOfTheRing.Fellowships.Api;
using BoardGame.WarOfTheRing.Fellowships.Api.EndpointMappings;
using BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;
using Serilog;
using Serilog.Formatting.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.RegisterFellowshipServices(builder.Configuration);

var app = builder.Build();
app.UseSerilogRequestLogging();

if (app.Environment.ApplicationIsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.ApplicationIsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.RegisterFellowshipEndpoints();
app.RegisterHuntEndpoints();

app.Run();