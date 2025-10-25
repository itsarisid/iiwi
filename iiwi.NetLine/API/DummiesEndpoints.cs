using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.Model.PingPong;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

public class DummiesEndpoints : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(DummiesDoc.Group)
            .AddEndpointFilter<ExceptionHandlingFilter>();

        routeGroup.MapVersionedEndpoint(new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = DummiesDoc.TestEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                CachePolicy = CachePolicy.NoCache,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });

        routeGroup.MapVersionedEndpoint(new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = DummiesDoc.AuthTestEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                AuthorizationPolicies = [Permissions.Test.Read],
                CachePolicy = CachePolicy.DefaultPolicy,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });
    }
}
