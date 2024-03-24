using BoardGame.WarOfTheRing.Dice.Api.EndpointMappings;
using BoardGame.WarOfTheRing.Dice.Api.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

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