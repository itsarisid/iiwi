
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Settings;

public sealed class SettingsOptions
{
    public const string ConfigurationSectionName = "Application";

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
    public required string Name { get; set; }
}
