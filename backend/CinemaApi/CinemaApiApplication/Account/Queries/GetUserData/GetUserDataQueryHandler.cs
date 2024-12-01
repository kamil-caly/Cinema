using CinemaApiApplication.Seance.Queries;
using CinemaApiApplication.Seance;
using CinemaApiDomain.Interfaces;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CinemaApiApplication.Account.Queries.GetUserData
{
    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataDto>
    {
        private readonly IConfiguration _configuration;

        public GetUserDataQueryHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<UserDataDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request._token))
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "null");

            var validatedToken = await Task.Run(() =>
            {
                tokenHandler.ValidateToken(request._token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"] ?? "",
                    ValidAudience = _configuration["Jwt:Audience"] ?? "",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedSecurityToken);

                return validatedSecurityToken;
            });

            var jwtToken = (JwtSecurityToken)validatedToken;

            var emailClaim = GetClaimFromToken("Email", jwtToken);
            var firstNameClaim = GetClaimFromToken("FirstName", jwtToken);
            var lastNameClaim = GetClaimFromToken("LastName", jwtToken);
            var roleClaim = GetClaimFromToken(ClaimTypes.Role, jwtToken);

            if (string.IsNullOrEmpty(emailClaim) || string.IsNullOrEmpty(firstNameClaim)
                || string.IsNullOrEmpty(lastNameClaim) || string.IsNullOrEmpty(roleClaim))
            {
                throw new UnauthorizedAccessException("Invalid token: Email or FirstName or LastName or Role not exists in token.");
            }

            return new UserDataDto
            {
                Email = emailClaim,
                FirstName = firstNameClaim,
                LastName = lastNameClaim,
                Role = roleClaim
            };
        }

        private string? GetClaimFromToken(string claimType, JwtSecurityToken jwtToken)
        {
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
