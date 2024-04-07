using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;
using BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;
using NServiceBus;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging.NServiceBus;

public static class EventMapper
{
    public static IEvent MapEvent(IntegrationEvent integrationEvent)
    {
        if (integrationEvent is FellowshipDeclaredInCityOrStrongholdIntegrationEvent fellowshipDeclaredInCityOrStronghold)
        {
            return new FellowshipDeclaredInCityOrStrongholdNServiceBusEvent()
            {
                GameId = fellowshipDeclaredInCityOrStronghold.GameId,
                OccurredOn = fellowshipDeclaredInCityOrStronghold.DateOccurred,
                NationName = fellowshipDeclaredInCityOrStronghold.NationName
            };
        }

        return null;
    }
}