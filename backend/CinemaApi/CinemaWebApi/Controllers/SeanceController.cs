using CinemaApiApplication.Seance;
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
        public async Task<ActionResult<IEnumerable<SeanceDto>>> GetAllForGivenDate([FromQuery] DateTime dateTime, [FromQuery] string? movieTitle)
        {
            var seancesDto = await _mediator.Send(new GetAllSeancesWithDetailsForGivenArgsQuery(dateTime, movieTitle)); 
            return Ok(seancesDto);
        }
    }
}
