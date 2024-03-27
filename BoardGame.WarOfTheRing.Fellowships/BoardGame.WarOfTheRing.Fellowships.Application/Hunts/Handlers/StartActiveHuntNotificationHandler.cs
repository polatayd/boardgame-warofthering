using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Handlers;

public class StartActiveHuntNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipProgressCounterForwarded>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IHuntRepository huntRepository;
    private readonly ILogger<StartActiveHuntNotificationHandler> logger;

    public StartActiveHuntNotificationHandler(IUnitOfWork unitOfWork, IHuntRepository huntRepository, ILogger<StartActiveHuntNotificationHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.huntRepository = huntRepository;
        this.logger = logger;
    }
    
    public async Task Handle(DomainEventNotification<FellowshipProgressCounterForwarded> notification, CancellationToken cancellationToken)
    {
        try
        {
            var fellowshipForwarded = notification.DomainEvent;

            var hunting = huntRepository.GetById(fellowshipForwarded.HuntingId);

            hunting.StartActiveHunt();

            await unitOfWork.SaveChangesAsync();
        }
        catch (HuntStateException e)
        {
            logger.LogError("Message:{message}", e.Message);
        }
    }
}