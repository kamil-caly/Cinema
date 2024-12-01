using CinemaApiApplication.Movie.Queries;
using CinemaApiApplication.Movie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CinemaApiApplication.Account.Commands.RegisterUser;
using FluentValidation;
using CinemaApiApplication.Account.Commands.LoginUser;
using CinemaApiApplication.Account;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CinemaApiApplication.Seance.Queries;
using System;
using CinemaApiApplication.Account.Queries.GetUserData;

namespace CinemaWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<RegisterUserCommand> _validator;

        public AccountController(IMediator mediator, IValidator<RegisterUserCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterUserCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await _mediator.Send(command);

            return Results.Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserCommand command)
        {
            try
            {
                string token = await _mediator.Send(command);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getUserData")]
        public async Task<ActionResult<UserDataDto>> GetUserData([FromQuery] string token)
        {
            try
            {
                UserDataDto userData = await _mediator.Send(new GetUserDataQuery(token));
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return StatusCode(401, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
