using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Handlers;

public class FellowshipDeclaredNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipDeclared>>
{
    private readonly IMessageService messageService;

    public FellowshipDeclaredNotificationHandler(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    public Task Handle(DomainEventNotification<FellowshipDeclared> notification, CancellationToken cancellationToken)
    {
        var fellowshipDeclared = notification.DomainEvent;

        messageService.SendAsync(new FellowshipDeclaredIntegrationEvent(fellowshipDeclared.GameId, fellowshipDeclared.NationName));
        
        return Task.CompletedTask;
    }
}