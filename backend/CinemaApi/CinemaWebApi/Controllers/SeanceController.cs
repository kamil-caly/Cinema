using CinemaApiApplication.Movie;
using CinemaApiApplication.Seance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers
{
    [Route("api/seance")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeanceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("getAllForGivenDate")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllForGivenDate([FromQuery] DateTime dateTime)
        {
            var seancesDto = await _mediator.Send(new GetAllSeancesForGivenDateQuery(dateTime)); 

            return Ok(seancesDto);
        }
    }
}
