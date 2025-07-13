namespace iiwi.Application.Authorization
{
    public record RoleResponse:Response
    {
       public List<Role> Roles { get; set; }
    }

    public record Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
