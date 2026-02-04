using DotNetCore.Domain;
using iiwi.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Domain;

public abstract class BaseEntity : Entity
{
    /// <summary>
    /// Gets or sets the CreationDate.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the UpdateDate.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// Gets or sets the IsDeleted.
    /// </summary>
    public bool? IsDeleted { get; set; }
    /// <summary>
    /// Gets or sets the IsActive.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the DeletedDate.
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// Gets or sets the CreatedByUser.
    /// </summary>
    public virtual ApplicationUser CreatedByUser { get; set; }
    /// <summary>
    /// Gets or sets the DeletedByUser.
    /// </summary>
    public virtual ApplicationUser DeletedByUser { get; set; }
    /// <summary>
    /// Gets or sets the UpdateByUser.
    /// </summary>
    public virtual ApplicationUser UpdateByUser { get; set; }
}

