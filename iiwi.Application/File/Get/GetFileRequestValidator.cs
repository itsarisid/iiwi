using FluentValidation;
using iiwi.Application.File;

namespace iiwi.Application.Fil;

public sealed class GetFileRequestValidator : AbstractValidator<GetFileRequest>
{
    public GetFileRequestValidator() => RuleFor(request => request.Id).Guid();
}

