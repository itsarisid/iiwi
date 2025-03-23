
using System.ComponentModel.DataAnnotations;

namespace Architecture.Application.Authentication
{
    public record DeletePersonalDataRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
