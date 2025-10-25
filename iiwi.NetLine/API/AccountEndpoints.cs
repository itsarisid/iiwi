
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

        routeGroup.MapVersionedEndpoint(new Configure<DownloadPersonalDataRequest, Response>
        {
            EndpointDetails = AccountsDoc.DownloadPersonalData,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.DownloadInfo],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        routeGroup.MapVersionedEndpoint(new Configure<ChangeEmailRequest, Response>
        {
            EndpointDetails = AccountsDoc.ChangeEmail,
            HttpMethod = HttpVerb.Put,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.ChangeEmail],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        routeGroup.MapVersionedEndpoint(new Configure<DeletePersonalDataRequest, Response>
        {
            EndpointDetails = AccountsDoc.DeletePersonalData,
            HttpMethod = HttpVerb.Delete,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.DeletePersonalData],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        routeGroup.MapVersionedEndpoint(new Configure<UpdatePhoneNumberRequest, Response>
        {
            EndpointDetails = AccountsDoc.UpdatePhoneNumber,
            HttpMethod = HttpVerb.Put,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.UpdatePhoneNumber],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}
