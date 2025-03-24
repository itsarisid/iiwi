﻿using DotNetCore.AspNetCore;
using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.AppWire.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class AccountController : BaseController
{
    /// <summary>Changes the password.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("change-password")]
    public IActionResult ChangePassword(ChangePasswordRequest request) => Mediator.HandleAsync<ChangePasswordRequest, Response>(request).ApiResult();

    /// <summary>Deleting this data will permanently remove your account, and this cannot be recovered.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpDelete("delete-personal-data")]
    public IActionResult DeletePersonalData(DeletePersonalDataRequest request) => Mediator.HandleAsync<DeletePersonalDataRequest, Response>(request).ApiResult();

    /// <summary>Downloads the personal data.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("download-personal-data")]
    public IActionResult DownloadPersonalData() => Mediator.HandleAsync<DownloadPersonalDataRequest, Response>(new DownloadPersonalDataRequest()).ApiResult();

    /// <summary>Changes the email.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("change-email")]
    public IActionResult ChangeEmail(ChangeEmailRequest request) => Mediator.HandleAsync<ChangeEmailRequest, Response>(request).ApiResult();

    /// <summary>Verifications the email.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("send-verification-email")]
    public IActionResult SendVerificationEmail() => Mediator.HandleAsync<SendVerificationEmailRequest, Response>(new SendVerificationEmailRequest()).ApiResult();
}
