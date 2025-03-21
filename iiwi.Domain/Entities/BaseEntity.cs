using DotNetCore.Domain;
using iiwi.Domain.Identity;

namespace iiwi.Domain;

public abstract class BaseEntity:Entity
{
    public DateTime CreationDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public Guid UpdateByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedDate { get; set; }
    public Guid DeletedByUserId { get; set; }

    public virtual ApplicationUser? CreatedByUser { get; set; }
    public virtual ApplicationUser? DeletedByUser { get; set; }
    public virtual ApplicationUser? UpdateByUser { get; set; }
}

