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

        // Map the endpoints to the route group
        var routeGroup = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Authentication.Group)
            .RequireAuthorization();

        /// <summary>
        /// Generates a shared key and QR code URI for the user's authenticator app.
        /// </summary>
        routeGroup.MapGet(Authentication.LoadKeyAndQrCodeUri.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>(new LoadKeyAndQrCodeUriRequest())
            .Response())
            .WithDocumentation(Authentication.LoadKeyAndQrCodeUri);

        /// <summary>
        /// Enables two-factor authentication using the provided code and shared key.
        /// </summary>
        routeGroup.MapPost(Authentication.EnableAuthenticator.Endpoint,
            IResult (IMediator mediator, EnableAuthenticatorRequest request) => mediator
            .HandleAsync<EnableAuthenticatorRequest, EnableAuthenticatorResponse>(request)
            .Response())
            .WithDocumentation(Authentication.EnableAuthenticator);

        /// <summary>
        /// Lists all external login providers linked to the user's account.
        /// </summary>
        routeGroup.MapGet(Authentication.ExternalLogins.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ExternalLoginsRequest, ExternalLoginsResponse>(new ExternalLoginsRequest())
            .Response())
            .WithDocumentation(Authentication.ExternalLogins);

        /// <summary>
        /// Unlinks a specified external login provider from the user's account.
        /// </summary>
        routeGroup.MapDelete(Authentication.RemoveLogin.Endpoint,
            IResult (IMediator mediator, [FromBody] RemoveLoginRequest request) => mediator
            .HandleAsync<RemoveLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.RemoveLogin);

        /// <summary>
        /// Initiates a link between an external login provider and the current user.
        /// </summary>
        routeGroup.MapPost(Authentication.LinkLogin.Endpoint,
            IResult (IMediator mediator, LinkLoginRequest request) => mediator
            .HandleAsync<LinkLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.LinkLogin);

        /// <summary>
        /// Callback endpoint after an external login is linked to the account.
        /// </summary>
        routeGroup.MapGet(Authentication.LinkLoginCallback.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LinkLoginCallbackRequest, Response>(new LinkLoginCallbackRequest())
            .Response())
            .WithDocumentation(Authentication.LinkLoginCallback);

        /// <summary>
        /// Generates recovery codes for account access when 2FA is unavailable.
        /// </summary>
        routeGroup.MapPost(Authentication.GenerateRecoveryCodes.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>(new GenerateRecoveryCodesRequest())
            .Response())
            .WithDocumentation(Authentication.GenerateRecoveryCodes);

        /// <summary>
        /// Resets the authenticator key for the user, requiring re-setup of 2FA.
        /// </summary>
        routeGroup.MapPost(Authentication.ResetAuthenticator.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ResetAuthenticatorRequest, Response>(new ResetAuthenticatorRequest())
            .Response())
            .WithDocumentation(Authentication.ResetAuthenticator);

        /// <summary>
        /// Allows the user to set a password if one is not already configured.
        /// </summary>
        routeGroup.MapPost(Authentication.SetPassword.Endpoint,
            IResult (IMediator mediator, SetPasswordRequest request) => mediator
            .HandleAsync<SetPasswordRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.SetPassword);

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        routeGroup.MapPost(Authentication.ChangePassword.Endpoint,
         IResult (IMediator mediator, ChangePasswordRequest request) => mediator
        .HandleAsync<ChangePasswordRequest, Response>(request)
        .Response())
        .WithDocumentation(Authentication.ChangePassword);

        /// <summary>
        /// Retrieves the current authentication and security status of the account.
        /// </summary>
        routeGroup.MapPost(Authentication.AccountStatus.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<AccountStatusRequest, Response>(new AccountStatusRequest())
            .Response())
            .WithDocumentation(Authentication.AccountStatus);

        /// <summary>
        /// Marks the current browser as forgotten, requiring re-authentication in future sessions.
        /// </summary>
        routeGroup.MapDelete(Authentication.ForgotBrowser.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ForgotBrowserRequest, Response>(new ForgotBrowserRequest())
            .Response())
            .WithDocumentation(Authentication.ForgotBrowser);

        /// <summary>
        /// Disables two-factor authentication for the user.
        /// </summary>
        routeGroup.MapPost(Authentication.Disable2fa.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<Disable2faRequest, Response>(new Disable2faRequest())
            .Response())
            .WithDocumentation(Authentication.Disable2fa);
    }
}
