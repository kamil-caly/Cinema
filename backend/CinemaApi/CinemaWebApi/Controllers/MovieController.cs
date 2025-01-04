using CinemaApiApplication.Movie;
using CinemaApiApplication.Movie.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
        {
            var moviesDto = await _mediator.Send(new GetAllMoviesQuery());
            return Ok(moviesDto);   
        }
    }
}
