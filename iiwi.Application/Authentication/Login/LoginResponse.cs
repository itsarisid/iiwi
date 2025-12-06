
using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;
/// <summary>
/// Response model for user login.
/// </summary>
public record LoginResponse : Response
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the authentication token.
    /// </summary>
    public string Token { get; set; }
}
