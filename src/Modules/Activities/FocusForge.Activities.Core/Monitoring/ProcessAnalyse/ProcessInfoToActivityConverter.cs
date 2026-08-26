using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    public class ProcessInfoToActivityConverter
    {
        private readonly DefaultActivityDescription _defaultDescription = new DefaultActivityDescription();
        private readonly List<IProcessDescription> _processDescriptions = new List<IProcessDescription>();

        public UserActivity Convert(ActiveProcessInfo processInfo)
        {
            var descriptor = _processDescriptions.SingleOrDefault(p => p.Name == processInfo.ExecutableName);
            if(descriptor is null)
                descriptor = _defaultDescription;
            return descriptor.Describe(processInfo);
        }

        public void RegisterProcessDescription(IProcessDescription processDescription)
        {
            _processDescriptions.Add(processDescription);
        }
    }
}
