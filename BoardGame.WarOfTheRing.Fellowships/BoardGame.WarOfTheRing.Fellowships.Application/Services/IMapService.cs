using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMapService
{
    public Task<int> SendReRollCountRequestAsync(Guid gameId);
    public Task<Region> SendMoveFellowshipRequestAsync(Guid gameId);
}