using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;
using BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging;

public static class EventMapper
{
    public static IIntegrationEvent MapEvent(IntegrationEvent integrationEvent)
    {
        if (integrationEvent is FellowshipDeclaredIntegrationEvent fellowshipDeclared)
        {
            return new FellowshipDeclaredEvent()
            {
                GameId = fellowshipDeclared.GameId,
                OccurredOn = fellowshipDeclared.DateOccurred,
                NationName = fellowshipDeclared.NationName
            };
        }

        return null;
    }
}