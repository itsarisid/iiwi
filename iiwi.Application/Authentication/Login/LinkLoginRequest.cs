using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public record LinkLoginRequest
    {
        [Required]
        public string Provider { get; set; }
    }
}
