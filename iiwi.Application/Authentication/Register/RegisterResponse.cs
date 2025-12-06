using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication;

/// <summary>
/// Response model for user registration.
/// </summary>
public record RegisterResponse: Response
{
    /// <summary>
    /// Gets or sets the full name.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the JWT token.
    /// </summary>
    public JwtSecurityToken Token { get; set; }
}
