using FluentValidation;

namespace iiwi.Application.File;

public sealed class AddFileRequestValidator : AbstractValidator<AddFileRequest>
{
    public AddFileRequestValidator() => RuleFor(request => request.Files).Files();
}

