using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class EnableAuthenticatorModel
{
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Verification Code")]
    public string Code { get; set; }
}

/// <summary>MFA Response</summary>
public class EnableAuthenticatorResponse
{
    /// <summary>Gets or sets the shared key.</summary>
    /// <value>The shared key.</value>
    public string SharedKey { get; set; }

    /// <summary>Gets or sets the authenticator URI.</summary>
    /// <value>The authenticator URI.</value>
    public string AuthenticatorUri { get; set; }

    /// <summary>Gets or sets the recovery codes.</summary>
    /// <value>The recovery codes.</value>
    public string[] RecoveryCodes { get; set; }

    /// <summary>Gets or sets the status message.</summary>
    /// <value>The status message.</value>
    public string StatusMessage { get; set; }
}