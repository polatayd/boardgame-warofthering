using NServiceBus;

// ReSharper disable once CheckNamespace
namespace BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;

public class FellowshipDeclaredInCityOrStrongholdNServiceBusEvent : IEvent
{
    public Guid GameId { get; set; }
    public DateTime OccurredOn { get; set; }
    public string NationName { get; set; }
}