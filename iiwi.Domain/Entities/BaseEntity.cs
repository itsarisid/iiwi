using DotNetCore.Domain;
using iiwi.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Domain;

public abstract class BaseEntity : Entity
{
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }
    //public Guid CreatedByUserId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? UpdateDate { get; set; }
    //public Guid? UpdateByUserId { get; set; }
    public bool? IsDeleted { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DeletedDate { get; set; }
    //public Guid? DeletedByUserId { get; set; }

    public virtual ApplicationUser? CreatedByUser { get; set; }
    public virtual ApplicationUser? DeletedByUser { get; set; }
    public virtual ApplicationUser? UpdateByUser { get; set; }
}

