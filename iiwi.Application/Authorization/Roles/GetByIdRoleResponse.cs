using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiwi.Application.Authorization
{
        public record GetByIdRoleResponse : Response
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
