using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Handlers;

public class FellowshipDeclaredInCityOrStrongholdNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipDeclaredInCityOrStronghold>>
{
    private readonly IMessageService messageService;

    public FellowshipDeclaredInCityOrStrongholdNotificationHandler(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    public Task Handle(DomainEventNotification<FellowshipDeclaredInCityOrStronghold> notification, CancellationToken cancellationToken)
    {
        var fellowshipDeclared = notification.DomainEvent;

        messageService.SendAsync(new FellowshipDeclaredInCityOrStrongholdIntegrationEvent(fellowshipDeclared.GameId, fellowshipDeclared.NationName));
        
        return Task.CompletedTask;
    }
}