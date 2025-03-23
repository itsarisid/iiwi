// Ignore Spelling: Validator

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
    {
        public ChangeEmailValidator()
        {
            RuleFor(request => request.NewEmail).Email();
        }
    }
}
