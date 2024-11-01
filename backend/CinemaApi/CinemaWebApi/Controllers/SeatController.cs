using CinemaApiApplication.Movie.Queries;
using CinemaApiApplication.Movie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CinemaApiApplication.Seat;
using CinemaApiApplication.Seat.Queries.GetAllSeatsForGivenSeanceDate;

namespace CinemaWebApi.Controllers
{
    [Route("api/seat")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAllForGivenSeance")]
        public async Task<ActionResult<IEnumerable<SeatDto>?>> GetAllForGivenSeance([FromQuery] DateTime dateTime)
        {
            var seatsDto = await _mediator.Send(new GetAllSeatsForGivenSeanceDateQuery(dateTime));

            return Ok(seatsDto);
        }
    }
}
