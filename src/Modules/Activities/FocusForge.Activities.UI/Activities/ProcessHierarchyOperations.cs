using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using FocusForge.Activities.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    internal class ProcessHierarchyOperations
    {
        public void Upsert(ObservableCollection<ActivityViewModel> activities, UserActivity userActivity, TimeSpan timeSpan)
        {
            if (activities is null)
                throw new ArgumentNullException(nameof(activities));
            if (userActivity is null)
                throw new ArgumentNullException(nameof(userActivity));

            // Find or create top-level activity (Application)
            var top = activities.FirstOrDefault(a => a.Activity == userActivity.ApplicationName);
            if (top is null)
            {
                top = new ActivityViewModel
                {
                    Activity = userActivity.ApplicationName,
                    InnerActivities = new ObservableCollection<ActivityViewModel>()
                };
                activities.Add(top);
            }

            // Add time to top
             top.Time += timeSpan;

            // If there's a second-level description, find or create it under top
            if (!string.IsNullOrWhiteSpace(userActivity.ActivityDescription))
            {
                var second = top.InnerActivities.FirstOrDefault(a => a.Activity == userActivity.ActivityDescription);
                if (second is null)
                {
                    second = new ActivityViewModel
                    {
                        Activity = userActivity.ActivityDescription,
                        InnerActivities = []
                    };
                    top.InnerActivities.Add(second);
                }

                // Add time to second
                second.Time += timeSpan;

                // If there's an inner description, find or create it under second
                if (!string.IsNullOrWhiteSpace(userActivity.InnerActivityDescription))
                {
                    var third = second.InnerActivities.FirstOrDefault(a => a.Activity == userActivity.InnerActivityDescription);
                    if (third is null)
                    {
                        third = new ActivityViewModel
                        {
                            Activity = userActivity.InnerActivityDescription,
                            InnerActivities = []
                        };
                        second.InnerActivities.Add(third);
                    }

                    // Add time to third
                    third.Time += timeSpan;
                }
            }
            else if (!string.IsNullOrWhiteSpace(userActivity.InnerActivityDescription))
            {
                // No middle level but there's an inner description — place it as a child of top
                var second = top.InnerActivities.FirstOrDefault(a => a.Activity == userActivity.InnerActivityDescription);
                if (second is null)
                {
                    second = new ActivityViewModel
                    {
                        Activity = userActivity.InnerActivityDescription,
                        InnerActivities = []
                    };
                    top.InnerActivities.Add(second);
                }

                // Add time to the created/found child
                second.Time += timeSpan;
            }
        }
    }
}
