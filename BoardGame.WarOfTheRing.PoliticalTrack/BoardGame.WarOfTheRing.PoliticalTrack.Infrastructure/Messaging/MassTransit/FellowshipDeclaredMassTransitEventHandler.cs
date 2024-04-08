using BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using MassTransit;
using MediatR;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Messaging.MassTransit;

public class FellowshipDeclaredMassTransitEventHandler : IConsumer<FellowshipDeclaredEvent>
{
    private readonly IMediator mediator;

    public FellowshipDeclaredMassTransitEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Consume(ConsumeContext<FellowshipDeclaredEvent> context)
    {
        await mediator.Send(new ActivateNationCommand(new ActivateNationCommandInput()
        {
            Name = context.Message.NationName,
            GameId = context.Message.GameId
        }), context.CancellationToken);
    }
}