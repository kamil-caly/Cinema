﻿using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Seance.Queries;

public class GetAllSeancesWithDetailsForGivenArgsQueryHandler : IRequestHandler<GetAllSeancesWithDetailsForGivenArgsQuery, IEnumerable<SeanceDto>?>
{
    private readonly ISeanceRepository _seanceRepository;

    public GetAllSeancesWithDetailsForGivenArgsQueryHandler(ISeanceRepository seanceRepository)
    {
        this._seanceRepository = seanceRepository;
    }
    public async Task<IEnumerable<SeanceDto>?> Handle(GetAllSeancesWithDetailsForGivenArgsQuery request, CancellationToken cancellationToken)
    {
        var seances = await _seanceRepository.GetAllWithDetailsForGivenArgs(request._dateTime, request._movieTitle);

        if (seances != null)
        {
            return seances.Select(seance => new SeanceDto(seance, seance.Movie, seance.Hall)).ToList();
        }

        return null;
    }
}