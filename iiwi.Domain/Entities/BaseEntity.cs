using DotNetCore.Domain;
using iiwi.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Domain;

public abstract class BaseEntity : Entity
{
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }
    public bool IsActive { get; set; } = true;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DeletedDate { get; set; }

    public virtual ApplicationUser CreatedByUser { get; set; }
    public virtual ApplicationUser DeletedByUser { get; set; }
    public virtual ApplicationUser UpdateByUser { get; set; }
}

