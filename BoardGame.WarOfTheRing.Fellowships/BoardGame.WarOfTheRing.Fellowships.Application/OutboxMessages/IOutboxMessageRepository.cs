namespace BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;

public interface IOutboxMessageRepository
{
    public void Add(OutboxMessage outboxMessage);
    public IReadOnlyList<OutboxMessage> Get();
}