using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    public interface IProcessDescription
    {
        string Name { get; }
        UserActivity Describe(ActiveProcessInfo processInfo);
    }
}
