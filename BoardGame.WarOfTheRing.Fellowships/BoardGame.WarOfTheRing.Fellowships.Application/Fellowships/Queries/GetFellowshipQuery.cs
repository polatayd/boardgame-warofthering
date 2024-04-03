using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Outputs;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Queries;

public class GetFellowshipQuery(GetFellowshipQueryInput getFellowshipQuery) : IRequest<GetFellowshipQueryOutput>
{
    public GetFellowshipQueryInput Input { get; } = getFellowshipQuery;
}

public class GetFellowshipQueryHandler : IRequestHandler<GetFellowshipQuery, GetFellowshipQueryOutput>
{
    private readonly IFellowshipRepository fellowshipRepository;

    public GetFellowshipQueryHandler(IFellowshipRepository fellowshipRepository)
    {
        this.fellowshipRepository = fellowshipRepository;
    }

    public Task<GetFellowshipQueryOutput> Handle(GetFellowshipQuery request, CancellationToken cancellationToken)
    {
        var fellowship = fellowshipRepository.Get(request.Input.GameId);
        if (fellowship is null)
        {
            throw new FellowshipNotFoundException();
        }

        return Task.FromResult(new GetFellowshipQueryOutput()
        {
            CorruptionCounterValue = fellowship.CorruptionCounter.Value,
            ProgressCounterValue = fellowship.ProgressCounter.Value,
            Characters = fellowship.Characters.Select(x=>new CharacterOutput()
            {
                Level = x.Level,
                Name = x.Name.ToString(),
            }).ToList(),
            Guide = new CharacterOutput()
            {
                Level = fellowship.Guide.Level,
                Name = fellowship.Guide.Name.ToString()
            }
        });
    }
}