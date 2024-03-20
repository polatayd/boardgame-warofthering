using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    cfg.RegisterServicesFromAssembly(typeof(RollDicePoolRequest).GetTypeInfo().Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/dicepool", async ([FromBody] RollDicePoolInput rollDicePoolInput, [FromServices] IMediator mediator) =>
    {
        var result = await mediator.Send(new RollDicePoolRequest(rollDicePoolInput));
        return Results.Ok(result);
    })
    .WithName("RollDicePool")
    .WithOpenApi();

app.Run();