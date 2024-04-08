using BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Api.BackgroundServices;

public class PublishOutboxMessagesHostedService : BackgroundService
{
    private readonly TimeSpan period = TimeSpan.FromSeconds(5);
    private readonly ILogger<PublishOutboxMessagesHostedService> logger;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public PublishOutboxMessagesHostedService(IServiceScopeFactory serviceScopeFactory,
        ILogger<PublishOutboxMessagesHostedService> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await using var asyncServiceScope = serviceScopeFactory.CreateAsyncScope();
                var mediator = asyncServiceScope.ServiceProvider.GetService<IMediator>();

                logger.LogInformation("PublishOutboxMessagesHostedService started to publish");

                await mediator.Send(new PublishOutboxMessagesCommand(), stoppingToken);

                logger.LogInformation("PublishOutboxMessagesHostedService finished to publish");
            }
            catch (Exception e)
            {
                logger.LogError("Failed to execute PublishOutboxMessagesHostedService {Message}", e.Message);
            }
        }
    }
}