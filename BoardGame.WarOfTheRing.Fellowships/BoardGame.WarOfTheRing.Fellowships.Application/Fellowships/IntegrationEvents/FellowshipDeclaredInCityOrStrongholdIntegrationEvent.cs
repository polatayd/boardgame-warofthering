namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;

public class FellowshipDeclaredInCityOrStrongholdIntegrationEvent : IntegrationEvent
{
    public FellowshipDeclaredInCityOrStrongholdIntegrationEvent(Guid gameId, string nationName)
    {
        GameId = gameId;
        NationName = nationName;
    }

    public Guid GameId { get; init; }
    public string NationName { get; set; }
}