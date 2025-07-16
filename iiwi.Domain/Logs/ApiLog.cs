using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using DotNetCore.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace iiwi.Domain.Logs;

[AuditIgnore]
public class ApiLog : Entity
{
    [Key]
    public string TraceId { get; set; } // A unique identifier per request

    [Required]
    [MaxLength(10)]
    public string HttpMethod { get; set; } // HTTP method (GET, POST, etc)

    [Required]
    [MaxLength(100)]
    public string ControllerName { get; set; } // The controller name

    [Required]
    [MaxLength(100)]
    public string ActionName { get; set; } // The action name

    public string FormVariables { get; set; } // Form-data input variables passed to the action (serialized JSON)

    public string ActionParameters { get; set; } // The action parameters passed (serialized JSON)

    [MaxLength(100)]
    public string UserName { get; set; } // Username on the HttpContext Identity

    [Required]
    public string RequestUrl { get; set; } // URL of the request

    [MaxLength(50)]
    public string IpAddress { get; set; } // Client IP address

    public int ResponseStatusCode { get; set; } // HTTP response status code

    [MaxLength(100)]
    public string ResponseStatus { get; set; } // Response status description

    public virtual BodyContent RequestBody { get; set; } // The request body (optional)

    public virtual BodyContent ResponseBody { get; set; } // The response body (optional)

    public string Headers { get; set; } // HTTP Request Headers (serialized JSON, optional)

    public string ResponseHeaders { get; set; } // HTTP Response Headers (serialized JSON, optional)

    public bool ModelStateValid { get; set; } // Boolean to indicate if the model is valid

    public string ModelStateErrors { get; set; } // Error description when the model is invalid

    public string Exception { get; set; } // The exception thrown details (if any)

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Added for tracking when the log was created
}

[ComplexType]
public class BodyContent
{
    [MaxLength(50)]
    public string Type { get; set; } // The body type reported

    public long? Length { get; set; } // The length of the body if reported

    public string Value { get; set; } // The body content (serialized if complex object)
}
