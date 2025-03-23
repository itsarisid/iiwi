using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication.Mfa
{
    public record GenerateRecoveryCodesResponse:Response
    {
        public string[] RecoveryCodes { get; set; }
    }
}
