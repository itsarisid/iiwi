using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiwi.Common.Privileges;

public interface IPermissionsModule
{
    IEnumerable<string> All { get; }
}
