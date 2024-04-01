namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMapService
{
    public Task<int> SendRerollIsAvailableRequestAsync(Guid fellowshipId);
}