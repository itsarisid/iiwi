namespace iiwi.Model.Endpoints;

/// <summary>
/// Represents metadata for a single API endpoint used by NetLine/Carter endpoints.
/// </summary>
public class EndpointDetails
{
    /// <summary>
    /// Gets or sets the route or path of the endpoint (for example, "/test").
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the display name of the endpoint.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a short summary of what the endpoint does.
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets a longer description of the endpoint's purpose and behavior.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets tags associated with the endpoint (used for grouping or documentation).
    /// </summary>
    public string Tags { get; set; }
}