using Asp.Versioning;
using iiwi.Application;
using iiwi.Common;
using iiwi.Model.Enums;
using iiwi.Model.PingPong;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.Modules;

public class PingPongModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty)
            .WithGroup(PingPong.Group)
            .AddEndpointFilter<ExceptionHandlingFilter>();

        routeGroup.MapVersionedEndpoint<EmptyRequest, SystemInfoResponse>(
            new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = PingPong.SystemInfoEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                CachePolicy = CachePolicy.DefaultPolicy,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });

        routeGroup.MapVersionedEndpoint<EmptyRequest, SystemInfoResponse>(
            new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = PingPong.AuthTestEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                RequireAuthorization = true,
                AuthorizationPolicies = [Permissions.Test.Read],
                CachePolicy = CachePolicy.DefaultPolicy,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });
    }

}

