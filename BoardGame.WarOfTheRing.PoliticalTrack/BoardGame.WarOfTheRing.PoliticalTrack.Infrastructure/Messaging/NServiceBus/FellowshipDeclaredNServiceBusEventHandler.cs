using BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using MediatR;
using NServiceBus;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Messaging.NServiceBus;

public class FellowshipDeclaredMassTransitEventHandler : IHandleMessages<FellowshipDeclaredEvent>
{
    private readonly IMediator mediator;

    public FellowshipDeclaredMassTransitEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Handle(FellowshipDeclaredEvent message, IMessageHandlerContext context)
    {
        await mediator.Send(new ActivateNationCommand(new ActivateNationCommandInput()
        {
            Name = message.NationName,
            GameId = message.GameId
        }), context.CancellationToken);
    }
}