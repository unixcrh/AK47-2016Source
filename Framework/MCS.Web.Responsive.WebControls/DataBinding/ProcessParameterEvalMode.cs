using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.Responsive.WebControls
{
    [Flags]
    public enum ProcessParameterEvalMode
    {
        CurrentProcess = 1,

        ApprovalRootProcess = 2,
        
        RootProcess = 4,
        
        SameResourceRootProcess = 8,
    }
}
