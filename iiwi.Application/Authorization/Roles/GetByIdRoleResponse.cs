namespace iiwi.Application.Authorization
{
    /// <summary>
    /// Response model for getting a role by ID.
    /// </summary>
    public record GetByIdRoleResponse : Response
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }
    }
}
