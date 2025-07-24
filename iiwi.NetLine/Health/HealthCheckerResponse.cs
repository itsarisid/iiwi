using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text;

namespace iiwi.NetLine.Health;

/// <summary>
/// Provides methods for formatting health check results as JSON responses
/// </summary>
/// <remarks>
/// This static class handles the serialization of health check reports into
/// a standardized JSON format suitable for API responses. The output includes:
/// - Overall system status
/// - Individual check results
/// - Descriptions
/// - Additional health data
/// </remarks>
public static class HealthCheckerResponse
{
    /// <summary>
    /// Writes a health report as a JSON formatted HTTP response
    /// </summary>
    /// <param name="context">The HTTP context for the response</param>
    /// <param name="healthReport">The health report to serialize</param>
    /// <returns>A task representing the asynchronous write operation</returns>
    /// <remarks>
    /// <para>
    /// The response format includes:
    /// - Top-level status (aggregated from all checks)
    /// - Detailed results for each health check component
    /// - Descriptions of any issues
    /// - Additional diagnostic data
    /// </para>
    /// <para>
    /// Example response structure:
    /// <code>
    /// {
    ///   "status": "Healthy",
    ///   "results": {
    ///     "database": {
    ///       "status": "Healthy",
    ///       "description": "Connection successful",
    ///       "data": {
    ///         "connectionTime": "00:00:00.123"
    ///       }
    ///     },
    ///     "serviceX": {
    ///       "status": "Degraded",
    ///       "description": "High latency",
    ///       "data": {
    ///         "latency": 250,
    ///         "threshold": 200
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public static Task WriteResponse(HttpContext context, HealthReport healthReport)
    {
        // Set JSON content type with UTF-8 encoding
        context.Response.ContentType = "application/json; charset=utf-8";

        // Configure JSON writer for pretty-printed output
        var options = new JsonWriterOptions { Indented = true };

        // Use memory stream for efficient JSON construction
        using var memoryStream = new MemoryStream();
        using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
        {
            // Build the JSON structure
            jsonWriter.WriteStartObject();

            // Write overall system status
            jsonWriter.WriteString("status", healthReport.Status.ToString());

            // Begin results section
            jsonWriter.WriteStartObject("results");

            // Process each health check entry
            foreach (var healthReportEntry in healthReport.Entries)
            {
                jsonWriter.WriteStartObject(healthReportEntry.Key);

                // Write check-specific status
                jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());

                // Write description if available
                jsonWriter.WriteString("description", healthReportEntry.Value.Description);

                // Begin data section
                jsonWriter.WriteStartObject("data");

                // Write all additional data points
                foreach (var item in healthReportEntry.Value.Data)
                {
                    jsonWriter.WritePropertyName(item.Key);
                    JsonSerializer.Serialize(jsonWriter, item.Value,
                        item.Value?.GetType() ?? typeof(object));
                }

                jsonWriter.WriteEndObject(); // End data
                jsonWriter.WriteEndObject(); // End check entry
            }

            jsonWriter.WriteEndObject(); // End results
            jsonWriter.WriteEndObject(); // End root
        }

        // Write the JSON to the response
        return context.Response.WriteAsync(
            Encoding.UTF8.GetString(memoryStream.ToArray()));
    }
}