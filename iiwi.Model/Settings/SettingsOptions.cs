
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Settings;

public sealed class SettingsOptions
{
    public const string ConfigurationSectionName = "Application";

    [Required]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
    public required string Name { get; set; }
}
