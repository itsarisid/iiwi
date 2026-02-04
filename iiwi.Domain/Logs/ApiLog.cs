using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using DotNetCore.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace iiwi.Domain.Logs;

[AuditIgnore]
public class ApiLog : Entity
{
    /// <summary>
    /// Gets or sets the TraceId.
    /// </summary>
    [Key]
    public string TraceId { get; set; } // A unique identifier per request

    /// <summary>
    /// Gets or sets the HttpMethod.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string HttpMethod { get; set; } // HTTP method (GET, POST, etc)

    /// <summary>
    /// Gets or sets the ControllerName.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ControllerName { get; set; } // The controller name

    /// <summary>
    /// Gets or sets the ActionName.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ActionName { get; set; } // The action name

    /// <summary>
    /// Gets or sets the FormVariables.
    /// </summary>
    public string FormVariables { get; set; } // Form-data input variables passed to the action (serialized JSON)

    /// <summary>
    /// Gets or sets the ActionParameters.
    /// </summary>
    public string ActionParameters { get; set; } // The action parameters passed (serialized JSON)

    /// <summary>
    /// Gets or sets the UserName.
    /// </summary>
    [MaxLength(100)]
    public string UserName { get; set; } // Username on the HttpContext Identity

    /// <summary>
    /// Gets or sets the RequestUrl.
    /// </summary>
    [Required]
    public string RequestUrl { get; set; } // URL of the request

    /// <summary>
    /// Gets or sets the IpAddress.
    /// </summary>
    [MaxLength(50)]
    public string IpAddress { get; set; } // Client IP address

    /// <summary>
    /// Gets or sets the ResponseStatusCode.
    /// </summary>
    public int ResponseStatusCode { get; set; } // HTTP response status code

    /// <summary>
    /// Gets or sets the ResponseStatus.
    /// </summary>
    [MaxLength(100)]
    public string ResponseStatus { get; set; } // Response status description

    /// <summary>
    /// Gets or sets the RequestBody.
    /// </summary>
    public virtual BodyContent RequestBody { get; set; } // The request body (optional)

    /// <summary>
    /// Gets or sets the ResponseBody.
    /// </summary>
    public virtual BodyContent ResponseBody { get; set; } // The response body (optional)

    /// <summary>
    /// Gets or sets the Headers.
    /// </summary>
    public string Headers { get; set; } // HTTP Request Headers (serialized JSON, optional)

    /// <summary>
    /// Gets or sets the ResponseHeaders.
    /// </summary>
    public string ResponseHeaders { get; set; } // HTTP Response Headers (serialized JSON, optional)

    /// <summary>
    /// Gets or sets the ModelStateValid.
    /// </summary>
    public bool ModelStateValid { get; set; } // Boolean to indicate if the model is valid

    /// <summary>
    /// Gets or sets the ModelStateErrors.
    /// </summary>
    public string ModelStateErrors { get; set; } // Error description when the model is invalid

    /// <summary>
    /// Gets or sets the Exception.
    /// </summary>
    public string Exception { get; set; } // The exception thrown details (if any)

    /// <summary>
    /// Gets or sets the CreatedAt.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Added for tracking when the log was created
}

[ComplexType]
public class BodyContent
{
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [MaxLength(50)]
    public string Type { get; set; } // The body type reported

    /// <summary>
    /// Gets or sets the Length.
    /// </summary>
    public long? Length { get; set; } // The length of the body if reported

    /// <summary>
    /// Gets or sets the Value.
    /// </summary>
    public string Value { get; set; } // The body content (serialized if complex object)
}
