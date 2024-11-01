using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Seance.Queries;

public class GetAllSeancesForGivenDateQueryHandler : IRequestHandler<GetAllSeancesForGivenDateQuery, IEnumerable<SeanceDto>?>
{
    private readonly ISeanceRepository _seanceRepository;

    public GetAllSeancesForGivenDateQueryHandler(ISeanceRepository seanceRepository)
    {
        this._seanceRepository = seanceRepository;
    }
    public async Task<IEnumerable<SeanceDto>?> Handle(GetAllSeancesForGivenDateQuery request, CancellationToken cancellationToken)
    {
        var seances = await _seanceRepository.GetAllWithDetailsForGivenDate(request._dateTime);

        if (seances != null)
        {
            return seances.Select(seance => new SeanceDto(seance, seance.Movie, seance.Hall)).ToList();
        }

        return null;
    }
}
