using MediatR;

namespace CinemaApiApplication.Account.Commands.LoginUser
{
    public class LoginUserCommand : LoginUserDto, IRequest<string>
    {
    }
}
