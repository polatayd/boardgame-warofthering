namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMapService
{
    public Task<int> SendReRollCountRequestAsync(Guid gameId);
    public Task SendMoveFellowshipRequestAsync(Guid gameId);
}