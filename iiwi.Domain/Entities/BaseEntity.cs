using DotNetCore.Domain;
using iiwi.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Domain;

/// <summary>
/// Base entity class.
/// </summary>
public abstract class BaseEntity : Entity
{
    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool? IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the deleted date.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    public virtual ApplicationUser CreatedByUser { get; set; }

    /// <summary>
    /// Gets or sets the user who deleted the entity.
    /// </summary>
    public virtual ApplicationUser DeletedByUser { get; set; }

    /// <summary>
    /// Gets or sets the user who updated the entity.
    /// </summary>
    public virtual ApplicationUser UpdateByUser { get; set; }
}

