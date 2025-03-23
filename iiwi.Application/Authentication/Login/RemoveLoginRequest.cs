using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public record RemoveLoginRequest
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
