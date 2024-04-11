using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using MediatR;

namespace BoardGame.WarOfTheRing.Maps.Application.Maps.Queries;

public class GetMapQuery : IRequest<Map>
{
}

public class GetMapQueryHandler : IRequestHandler<GetMapQuery, Map>
{
    private readonly IMapRepository mapRepository;

    public GetMapQueryHandler(IMapRepository mapRepository)
    {
        this.mapRepository = mapRepository;
    }

    public async Task<Map> Handle(GetMapQuery request, CancellationToken cancellationToken)
    {
        var map = mapRepository.Get();

        return map;
    }
}