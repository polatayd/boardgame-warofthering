namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMapService
{
    public Task<int> SendReRollCountRequestAsync(Guid fellowshipId);
}