using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Factories;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Outputs;
using MediatR;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Queries;

public class GetNationRequest(GetNationRequestInput input) : IRequest<GetNationRequestOutput>
{
    public GetNationRequestInput Input { get; } = input;
}

public class GetNationRequestHandler : IRequestHandler<GetNationRequest, GetNationRequestOutput>
{
    private readonly INationRepository nationRepository;

    public GetNationRequestHandler(INationRepository nationRepository)
    {
        this.nationRepository = nationRepository;
    }

    public Task<GetNationRequestOutput> Handle(GetNationRequest request, CancellationToken cancellationToken)
    {
        var nation = nationRepository.Get(request.Input.Name, request.Input.GameId);

        if (nation is null)
        {
            return Task.FromResult((GetNationRequestOutput)null);
        }

        var nationOutput = new GetNationRequestOutput()
        {
            Name = nation.Name.Value,
            Position = nation.Position.Value,
            Status = NationFactory.CreateStatusOutput(nation.Status),
            IsAtWar = nation.IsAtWar()
        };
        
        return Task.FromResult(nationOutput);
    }
}