using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;

public record RegisterResponse: Response
{
    /// <summary>
    /// Gets or sets the FullName.
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// Gets or sets the Token.
    /// </summary>
    public JwtSecurityToken Token { get; set; }
}
