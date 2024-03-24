using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.PoliticalTrack.Application;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Queries;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
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

builder.Services.AddDbContext<PoliticalTrackDbContext>((_, optionsBuilder) =>
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgresql"))
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

app.MapPost("/nation",
        async ([FromBody] CreateNationCommandInput createNationCommandInput, [FromServices] IMediator mediator) =>
        {
            try
            {
                await mediator.Send(new CreateNationCommand(createNationCommandInput));
            }
            catch (NationAlreadyExistException e)
            {
                return Results.BadRequest(new
                {
                    error = e.Message,
                });
            }
            return Results.Created();
        })
    .WithName("CreateNation")
    .WithOpenApi();

app.MapGet("/nation", async ([FromQuery] string name, [FromQuery] Guid gameId, [FromServices] IMediator mediator) =>
    {
        var nation = await mediator.Send(new GetNationRequest(new GetNationRequestInput() { Name = name, GameId = gameId }));

        if (nation is null)
            return Results.NotFound();

        return Results.Ok(nation);
    })
    .WithName("GetNation")
    .WithOpenApi();

app.MapPut("/nation/activation",
        async ([FromBody] ActivateNationCommandInput activateNationCommandInput, [FromServices] IMediator mediator) =>
        {
            try
            {
                await mediator.Send(new ActivateNationCommand(activateNationCommandInput));
            }
            catch (NationNotFoundException)
            {
                return Results.NotFound();
            }

            return Results.Ok();
        })
    .WithName("ActivateNation")
    .WithOpenApi();

app.MapPost("/nation/advancement",
        async ([FromBody] AdvanceNationCommandInput advanceNationCommandInput, [FromServices] IMediator mediator) =>
        {
            try
            {
                await mediator.Send(new AdvanceNationCommand(advanceNationCommandInput));
            }
            catch (NationNotFoundException)
            {
                return Results.NotFound();
            }
            catch (PoliticalTrackAdvanceException e)
            {
                return Results.BadRequest(new
                {
                    error = e.Message,
                });
            }

            return Results.Ok();
        })
    .WithName("AdvanceNation")
    .WithOpenApi();

app.Run();