namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IMessageService
{
    public Task SendAsync(IntegrationEvent integrationEvent);
}