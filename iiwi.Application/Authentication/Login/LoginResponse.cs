
using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;
public record LoginResponse : Response
{
    /// <summary>
    /// Gets or sets the FullName.
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// Gets or sets the Token.
    /// </summary>
    public string Token { get; set; }
}
