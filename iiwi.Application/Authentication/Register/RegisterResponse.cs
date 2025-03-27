using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;

public record RegisterResponse: Response
{
    public string FullName { get; set; }
    public JwtSecurityToken Token { get; set; }
}
