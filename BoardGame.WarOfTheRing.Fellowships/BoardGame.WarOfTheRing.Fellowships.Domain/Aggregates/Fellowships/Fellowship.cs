using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Specifications;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;

public class Fellowship : EntityBase, IAggregateRoot
{
    public Guid GameId { get; private set; }
    public Guid HuntingId { get; private set; }
    public ProgressCounter ProgressCounter { get; private set; }
    public CorruptionCounter CorruptionCounter { get; private set; }

    private readonly List<Character> characters = new();
    public IReadOnlyList<Character> Characters => characters.AsReadOnly();
    public Character Guide => characters.FirstOrDefault();

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Fellowship()
    {
    }

    private Fellowship(Guid gameId)
    {
        Id = Guid.NewGuid();
        GameId = gameId;
        HuntingId = Guid.NewGuid();
        ProgressCounter = new ProgressCounter();
        CorruptionCounter = new CorruptionCounter();

        //TODO: Order of the characters should be get from player.
        characters =
        [
            Character.Gandalf,
            Character.Strider,
            Character.Boromir,
            Character.Legolas,
            Character.Gimli,
            Character.Pippin,
            Character.Merry,
        ];
    }

    public static Fellowship Create(Guid gameId)
    {
        var fellowship = new Fellowship(gameId);

        fellowship.RegisterDomainEvent(new FellowshipCreated(fellowship.Id, fellowship.HuntingId, fellowship.GameId));

        return fellowship;
    }

    public void ForwardProgressCounter(HuntState huntState)
    {
        var restriction = new ProgressCounterForwardRestriction(huntState);

        if (restriction.IsSatisfiedBy(this))
        {
            throw new FellowshipProgressCounterException(restriction.Message);
        }

        ProgressCounter = ProgressCounter.Forward();

        RegisterDomainEvent(new FellowshipProgressCounterForwarded(Id, HuntingId, ProgressCounter.Value));
    }

    public Character TakeGuideCasualty(int damage)
    {
        var casualty = Guide;
        if (casualty is null || casualty == Character.Gollum)
        {
            throw new CharacterNotAvailableException("Guide is not available for casualty");
        }

        characters.Remove(casualty);

        TakeCorruptionIfNecessary(damage, casualty.Level);

        RegisterDomainEvent(new CasualtyTaken(HuntingId));

        return casualty;
    }

    public Character TakeRandomCasualty(int damage)
    {
        if (characters.Count == 1 && Guide == Character.Gollum)
        {
            throw new CharacterNotAvailableException("Guide is not available for casualty");
        }

        var random = new Random();

        var casualtyIndex = random.Next(characters.Count);
        var casualty = characters[casualtyIndex];
        characters.RemoveAt(casualtyIndex);

        TakeCorruptionIfNecessary(damage, casualty.Level);
        
        RegisterDomainEvent(new CasualtyTaken(HuntingId));

        return casualty;
    }

    public Character TakeNoneCasualty(int damage)
    {
        TakeCorruptionIfNecessary(damage, 0);
        RegisterDomainEvent(new CasualtyTaken(HuntingId));

        return null;
    }
    
    private void TakeCorruptionIfNecessary(int damage, int casualtyLevel)
    {
        if (casualtyLevel >= damage)
        {
            return;
        }

        CorruptionCounter = CorruptionCounter.TakeDamage(damage - casualtyLevel);
        
        //TODO: Register damageTaken event, handle it and raise integration event if it reaches max level.
    }

    public void Reveal()
    {
        ProgressCounter = ProgressCounter.Reveal();
        
        RegisterDomainEvent(new FellowshipRevealed(HuntingId));
    }

    public void ResetProgressCounter()
    {
        ProgressCounter = ProgressCounter.MoveToZero();
    }
}