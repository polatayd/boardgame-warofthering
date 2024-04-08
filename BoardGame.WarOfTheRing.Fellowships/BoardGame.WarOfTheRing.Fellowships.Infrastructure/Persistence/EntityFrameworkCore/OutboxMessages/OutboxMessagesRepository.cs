using BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.OutboxMessages;

public class OutboxMessagesRepository(FellowshipDbContext fellowshipDbContext) : IOutboxMessageRepository
{
    public void Add(OutboxMessage outboxMessage)
    {
        fellowshipDbContext.OutboxMessages.Add(outboxMessage);
    }

    public IReadOnlyList<OutboxMessage> Get()
    {
        return fellowshipDbContext.OutboxMessages
            .Where(x => x.ProcessedOn == null)
            .Take(20)
            .ToList();
    }
}