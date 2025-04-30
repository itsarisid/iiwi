using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

/// <summary>
/// Defines and registers the routes for authentication-related API endpoints.
/// </summary>
public class AuthenticationModules : IEndpoints
{
    /// <summary>
    /// Configures the authentication-related API routes in the endpoint routing system.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure routes.</param>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Authentication.Group);

        /// <summary>
        /// Generates a shared key and QR code URI for the user's authenticator app.
        /// </summary>
        routeGroup.MapGet(Authentication.LoadKeyAndQrCodeUri.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>(new LoadKeyAndQrCodeUriRequest())
            .Response())
            .WithDocumentation(Authentication.LoadKeyAndQrCodeUri)
            .RequireAuthorization();

        /// <summary>
        /// Enables two-factor authentication using the provided code and shared key.
        /// </summary>
        routeGroup.MapPost(Authentication.EnableAuthenticator.Endpoint,
            IResult (IMediator mediator, EnableAuthenticatorRequest request) => mediator
            .HandleAsync<EnableAuthenticatorRequest, EnableAuthenticatorResponse>(request)
            .Response())
            .WithDocumentation(Authentication.EnableAuthenticator)
            .RequireAuthorization();

        /// <summary>
        /// Lists all external login providers linked to the user's account.
        /// </summary>
        routeGroup.MapGet(Authentication.ExternalLogins.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ExternalLoginsRequest, ExternalLoginsResponse>(new ExternalLoginsRequest())
            .Response())
            .WithDocumentation(Authentication.ExternalLogins)
            .RequireAuthorization();

        /// <summary>
        /// Unlinks a specified external login provider from the user's account.
        /// </summary>
        routeGroup.MapDelete(Authentication.RemoveLogin.Endpoint,
            IResult (IMediator mediator, [FromBody] RemoveLoginRequest request) => mediator
            .HandleAsync<RemoveLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.RemoveLogin)
            .RequireAuthorization();

        /// <summary>
        /// Initiates a link between an external login provider and the current user.
        /// </summary>
        routeGroup.MapPost(Authentication.LinkLogin.Endpoint,
            IResult (IMediator mediator, LinkLoginRequest request) => mediator
            .HandleAsync<LinkLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.LinkLogin)
            .RequireAuthorization();

        /// <summary>
        /// Callback endpoint after an external login is linked to the account.
        /// </summary>
        routeGroup.MapGet(Authentication.LinkLoginCallback.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LinkLoginCallbackRequest, Response>(new LinkLoginCallbackRequest())
            .Response())
            .WithDocumentation(Authentication.LinkLoginCallback)
            .RequireAuthorization();

        /// <summary>
        /// Generates recovery codes for account access when 2FA is unavailable.
        /// </summary>
        routeGroup.MapPost(Authentication.GenerateRecoveryCodes.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>(new GenerateRecoveryCodesRequest())
            .Response())
            .WithDocumentation(Authentication.GenerateRecoveryCodes)
            .RequireAuthorization();

        /// <summary>
        /// Resets the authenticator key for the user, requiring re-setup of 2FA.
        /// </summary>
        routeGroup.MapPost(Authentication.ResetAuthenticator.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ResetAuthenticatorRequest, Response>(new ResetAuthenticatorRequest())
            .Response())
            .WithDocumentation(Authentication.ResetAuthenticator)
            .RequireAuthorization();

        /// <summary>
        /// Allows the user to set a password if one is not already configured.
        /// </summary>
        routeGroup.MapPost(Authentication.SetPassword.Endpoint,
            IResult (IMediator mediator, SetPasswordRequest request) => mediator
            .HandleAsync<SetPasswordRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.SetPassword)
            .RequireAuthorization();

        /// <summary>
        /// Retrieves the current authentication and security status of the account.
        /// </summary>
        routeGroup.MapPost(Authentication.AccountStatus.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<AccountStatusRequest, Response>(new AccountStatusRequest())
            .Response())
            .WithDocumentation(Authentication.AccountStatus)
            .RequireAuthorization();

        /// <summary>
        /// Marks the current browser as forgotten, requiring re-authentication in future sessions.
        /// </summary>
        routeGroup.MapDelete(Authentication.ForgotBrowser.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ForgotBrowserRequest, Response>(new ForgotBrowserRequest())
            .Response())
            .WithDocumentation(Authentication.ForgotBrowser)
            .RequireAuthorization();

        /// <summary>
        /// Disables two-factor authentication for the user.
        /// </summary>
        routeGroup.MapPost(Authentication.Disable2fa.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<Disable2faRequest, Response>(new Disable2faRequest())
            .Response())
            .WithDocumentation(Authentication.Disable2fa)
            .RequireAuthorization();
    }
}
