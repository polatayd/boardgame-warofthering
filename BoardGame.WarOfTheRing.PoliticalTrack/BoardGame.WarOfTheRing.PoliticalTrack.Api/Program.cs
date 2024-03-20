using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.PoliticalTrack.Application;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Queries;
using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore;
using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.Nations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateNationCommand).GetTypeInfo().Assembly));

builder.Services.AddDbContext<PoliticalTrackDbContext>((provider, optionsBuilder) =>
        optionsBuilder.UseInMemoryDatabase("InMemoryDatabase")
            .EnableSensitiveDataLogging())
    .AddScoped<IUnitOfWork, PoliticalTrackDbContext>(x => x.GetRequiredService<PoliticalTrackDbContext>());

builder.Services.AddScoped<INationRepository, NationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/nation", async ([FromBody] CreateNationCommandInput createNationCommandInput, [FromServices] IMediator mediator) =>
    {
        await mediator.Send(new CreateNationCommand(createNationCommandInput));
        return Results.Created();
    })
    .WithName("CreateNation")
    .WithOpenApi();

app.MapGet("/nation", async ([FromQuery] string name, [FromServices] IMediator mediator) =>
    {
        var nation = await mediator.Send(new GetNationRequest(new GetNationRequestInput() { Name = name}));

        if (nation is null)
            return Results.NotFound();
        
        return Results.Ok(nation);
    })
    .WithName("GetNation")
    .WithOpenApi();

app.Run();