namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMapService
{
    public Task<bool> SendRerollIsAvailableRequestAsync(Guid fellowshipId);
}