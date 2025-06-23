using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
