using System.Reflection;
using BoardGame.WarOfTheRing.Maps.Api.EndpointMappings;
using BoardGame.WarOfTheRing.Maps.Application;
using BoardGame.WarOfTheRing.Maps.Application.Maps;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Commands;
using BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;
using BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.Maps;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateMapCommand).GetTypeInfo().Assembly));

builder.Services.AddDbContext<MapDbContext>((_, optionsBuilder) =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgresql"),
            options => { options.CommandTimeout(10); })
        .EnableSensitiveDataLogging())
    .AddScoped<IUnitOfWork, MapDbContext>(x => x.GetRequiredService<MapDbContext>()); ;

builder.Services.AddScoped<IMapRepository, MapRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterMapEndpoints();

app.Run();