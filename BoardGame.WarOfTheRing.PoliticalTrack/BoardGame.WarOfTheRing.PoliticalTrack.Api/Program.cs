using BoardGame.WarOfTheRing.PoliticalTrack.Api.EndpointMappings;
using BoardGame.WarOfTheRing.PoliticalTrack.Api.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterPoliticalTrackServices(builder.Configuration);

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

app.RegisterNationEndpoints();

app.Run();