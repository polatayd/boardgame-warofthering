using BoardGame.WarOfTheRing.Maps.Api.EndpointMappings;
using BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MapDbContext>((_, optionsBuilder) =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgresql"),
            options => { options.CommandTimeout(10); })
        .EnableSensitiveDataLogging());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterMapEndpoints();

app.Run();