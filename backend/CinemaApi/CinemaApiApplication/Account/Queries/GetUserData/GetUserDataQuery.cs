using MediatR;

namespace CinemaApiApplication.Account.Queries.GetUserData
{
    public class GetUserDataQuery : IRequest<UserDataDto>
    {
        public string _token { get; set; }

        public GetUserDataQuery(string token)
        {
            _token = token;
        }
    }
}
