
using iiwi.Application.Authentication;
using iiwi.Common;
using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.Model.PingPong;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

public class AccountEndpoints : IEndpoint
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

        routeGroup.MapVersionedEndpoint(new Configure<SendVerificationEmailRequest, Response>
        {
            EndpointDetails = AccountsDoc.SendVerificationDetails,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.SendVerificationDetails],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}
