using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Maps.Api.EndpointHandlers;

public static class MapHandlers
{
    public static async Task<Results<ProblemHttpResult, Created>> CreateMap([FromServices] MapDbContext dbContext)
    {
        var map = Map.Create();

        dbContext.Add(map);

        await dbContext.SaveChangesAsync();

        return TypedResults.Created();
    }
}