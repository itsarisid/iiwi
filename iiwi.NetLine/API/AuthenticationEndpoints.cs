using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

public class AuthenticationEndpoints : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(AccountsDoc.Group)
            .RequireAuthorization()
            .AddEndpointFilter<ExceptionHandlingFilter>();

        routeGroup.MapVersionedEndpoint(new Configure<UpdateProfileRequest, Response>
        {
            EndpointDetails = AccountsDoc.UpdateProfile,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.UpdateProfile],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}
