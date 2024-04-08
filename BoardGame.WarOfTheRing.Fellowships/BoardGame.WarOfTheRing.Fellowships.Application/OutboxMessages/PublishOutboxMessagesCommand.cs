using System.Text.Json;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;

public class PublishOutboxMessagesCommand : IRequest;

public class PublishOutboxMessagesCommandHandler : IRequestHandler<PublishOutboxMessagesCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IOutboxMessageRepository outboxMessageRepository;
    private readonly ILogger<PublishOutboxMessagesCommandHandler> logger;
    private readonly IMessageService messageService;

    public PublishOutboxMessagesCommandHandler(IOutboxMessageRepository outboxMessageRepository, IUnitOfWork unitOfWork, ILogger<PublishOutboxMessagesCommandHandler> logger, IMessageService messageService)
    {
        this.outboxMessageRepository = outboxMessageRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.messageService = messageService;
    }

    public async Task Handle(PublishOutboxMessagesCommand request, CancellationToken cancellationToken)
    {
        var outboxMessages = outboxMessageRepository.Get();

        foreach (var outboxMessage in outboxMessages)
        {
            var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(outboxMessage.Content);

            if (integrationEvent is null)
            {
                logger.LogError("OutboxMessage could not deserialized {Id}", outboxMessage.Id);
                continue;
            }

            try
            {
                try
                {
                    await messageService.SendAsync(integrationEvent);
                    outboxMessage.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception e)
                {
                    logger.LogError("IntegrationEvent could not send {Message}", e.Message);
                    outboxMessage.Error = e.Message;
                }
            
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError("OutboxMessage could not saved {Message}", e.Message);
                throw;
            }
        }
    }
}