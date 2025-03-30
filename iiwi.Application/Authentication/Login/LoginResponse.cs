
using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;
public record LoginResponse : Response
{
    public string FullName { get; set; }
    public string Token { get; set; }
}
