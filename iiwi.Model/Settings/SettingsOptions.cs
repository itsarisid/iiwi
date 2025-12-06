
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Settings;

/// <summary>
/// Represents settings options.
/// </summary>
public sealed class SettingsOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string ConfigurationSectionName = "Application";

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
    public required string Name { get; set; }
}
