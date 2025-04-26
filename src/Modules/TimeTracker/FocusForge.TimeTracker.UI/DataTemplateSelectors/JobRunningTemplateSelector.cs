using DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wachman.DataTemplateSelectors
{
    internal class JobRunningTemplateSelector : DataTemplateSelector
    {
        public DataTemplate JobRunningTemplate { get; set; }
        public DataTemplate JobNotRunningTemplate { get; set; }
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return JobNotRunningTemplate;
            if(item is Job job)
            {
                if(job.IsRunning)
                    return JobRunningTemplate;
            }
            
            return JobNotRunningTemplate;
        }
    }
}
