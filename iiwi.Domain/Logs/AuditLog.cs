﻿
using Audit.EntityFramework;
using DotNetCore.Domain;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Domain.Logs;


[AuditIgnore]
/// <summary>
/// Represents an audit log entry that tracks changes to entities in the system.
/// </summary>
public class AuditLog : Entity
{
    /// <summary>
    /// Gets or sets the serialized data that was changed in this audit entry.
    /// This typically contains the before/after state of the modified entity.
    /// </summary>
    public string ChangedData { get; set; }

    /// <summary>
    /// Gets or sets the type name of the entity that was audited.
    /// </summary>
    [MaxLength(100)]
    public string EntityType { get; set; }

    /// <summary>
    /// Gets or sets the primary key value of the entity that was audited.
    /// </summary>
    [MaxLength(128)]
    public string EntityName { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the audited action occurred.
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the type of action that was performed (e.g., Create, Update, Delete).
    /// </summary>
    [MaxLength(20)]
    public string ActionType { get; set; }
    
    /// <summary>
    /// Gets or sets the primary key of particular record.
    /// </summary>
    public string RecordId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who performed the audited action.
    /// This could be a username, email, or system user ID.
    /// </summary>
    [MaxLength(100)]
    public string PerformedBy { get; set; }
}
