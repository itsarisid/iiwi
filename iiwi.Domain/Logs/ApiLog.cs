using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using DotNetCore.Domain;


namespace iiwi.Domain.Logs;

/// <summary>
/// Represents an API log entry.
/// </summary>
[AuditIgnore]
public class ApiLog : Entity
{
    /// <summary>
    /// Gets or sets the trace ID.
    /// </summary>
    [Key]
    public string TraceId { get; set; } // A unique identifier per request

    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string HttpMethod { get; set; } // HTTP method (GET, POST, etc)

    /// <summary>
    /// Gets or sets the controller name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ControllerName { get; set; } // The controller name

    /// <summary>
    /// Gets or sets the action name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ActionName { get; set; } // The action name

    /// <summary>
    /// Gets or sets the form variables.
    /// </summary>
    public string FormVariables { get; set; } // Form-data input variables passed to the action (serialized JSON)

    /// <summary>
    /// Gets or sets the action parameters.
    /// </summary>
    public string ActionParameters { get; set; } // The action parameters passed (serialized JSON)

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [MaxLength(100)]
    public string UserName { get; set; } // Username on the HttpContext Identity

    /// <summary>
    /// Gets or sets the request URL.
    /// </summary>
    [Required]
    public string RequestUrl { get; set; } // URL of the request

    /// <summary>
    /// Gets or sets the IP address.
    /// </summary>
    [MaxLength(50)]
    public string IpAddress { get; set; } // Client IP address

    /// <summary>
    /// Gets or sets the response status code.
    /// </summary>
    public int ResponseStatusCode { get; set; } // HTTP response status code

    /// <summary>
    /// Gets or sets the response status description.
    /// </summary>
    [MaxLength(100)]
    public string ResponseStatus { get; set; } // Response status description

    /// <summary>
    /// Gets or sets the request body.
    /// </summary>
    public virtual BodyContent RequestBody { get; set; } // The request body (optional)

    /// <summary>
    /// Gets or sets the response body.
    /// </summary>
    public virtual BodyContent ResponseBody { get; set; } // The response body (optional)

    /// <summary>
    /// Gets or sets the request headers.
    /// </summary>
    public string Headers { get; set; } // HTTP Request Headers (serialized JSON, optional)

    /// <summary>
    /// Gets or sets the response headers.
    /// </summary>
    public string ResponseHeaders { get; set; } // HTTP Response Headers (serialized JSON, optional)

    /// <summary>
    /// Gets or sets a value indicating whether the model state is valid.
    /// </summary>
    public bool ModelStateValid { get; set; } // Boolean to indicate if the model is valid

    /// <summary>
    /// Gets or sets the model state errors.
    /// </summary>
    public string ModelStateErrors { get; set; } // Error description when the model is invalid

    /// <summary>
    /// Gets or sets the exception details.
    /// </summary>
    public string Exception { get; set; } // The exception thrown details (if any)

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Added for tracking when the log was created
}

/// <summary>
/// Represents body content in an API log.
/// </summary>
[ComplexType]
public class BodyContent
{
    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    [MaxLength(50)]
    public string Type { get; set; } // The body type reported

    /// <summary>
    /// Gets or sets the content length.
    /// </summary>
    public long? Length { get; set; } // The length of the body if reported

    /// <summary>
    /// Gets or sets the content value.
    /// </summary>
    public string Value { get; set; } // The body content (serialized if complex object)
}
