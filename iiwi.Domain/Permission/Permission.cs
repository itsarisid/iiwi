namespace iiwi.Domain;

public class Permission: BaseEntity
{
    public string CodeName { get; set; }
    public string Description { get; set; } = string.Empty;
}
