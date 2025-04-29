using iiwi.Application;
using iiwi.Application.Settings;

namespace iiwi.NetLine.Modules;

/// <summary>
/// Defines and registers the routes for user settings-related API endpoints.
/// </summary>
public class SettingsModules : IEndpoints
{
    /// <summary>
    /// Configures the settings-related API routes in the endpoint routing system.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure routes.</param>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Settings.Group);

        /// <summary>
        /// Retrieves the current user's application settings or preferences.
        /// </summary>
        routeGroup.MapGet(Settings.GetPreferences.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<GetPreferencesRequest, GetPreferencesResponse>(new GetPreferencesRequest())
            .Response())
            .WithDocumentation(Settings.GetPreferences)
            .RequireAuthorization();

        /// <summary>
        /// Updates application preferences such as theme, language, or notification settings.
        /// </summary>
        routeGroup.MapPut(Settings.UpdatePreferences.Endpoint,
            IResult (IMediator mediator, UpdatePreferencesRequest request) => mediator
            .HandleAsync<UpdatePreferencesRequest, Response>(request)
            .Response())
            .WithDocumentation(Settings.UpdatePreferences)
            .RequireAuthorization();

        /// <summary>
        /// Retrieves user-specific display or dashboard configuration settings.
        /// </summary>
        routeGroup.MapGet(Settings.GetLayoutSettings.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<GetLayoutSettingsRequest, GetLayoutSettingsResponse>(new GetLayoutSettingsRequest())
            .Response())
            .WithDocumentation(Settings.GetLayoutSettings)
            .RequireAuthorization();

        /// <summary>
        /// Updates user dashboard layout or UI preferences.
        /// </summary>
        routeGroup.MapPost(Settings.SaveLayoutSettings.Endpoint,
            IResult (IMediator mediator, SaveLayoutSettingsRequest request) => mediator
            .HandleAsync<SaveLayoutSettingsRequest, Response>(request)
            .Response())
            .WithDocumentation(Settings.SaveLayoutSettings)
            .RequireAuthorization();

        /// <summary>
        /// Resets all user settings to default values.
        /// </summary>
        routeGroup.MapPost(Settings.ResetPreferences.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ResetPreferencesRequest, Response>(new ResetPreferencesRequest())
            .Response())
            .WithDocumentation(Settings.ResetPreferences)
            .RequireAuthorization();
    }
}
