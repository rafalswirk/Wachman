using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.Activities.Core.Monitoring;
using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Windows.Threading;
using FocusForge.Activities.UI.Services;

namespace FocusForge.Activities.UI.ViewModels
{
    public class ActivitiesViewModel : ObservableObject, IDisposable
    {
        private readonly ActivityMonitor _activityMonitor;
        private readonly IRawActivitiesExportService _rawActivitiesExportService;
        private readonly HashSet<string> _allActivitiesBuffer;
        private readonly ProcessHierarchyOperations _hierarchyOperations = new();
        private readonly Dictionary<string, TimeSpan> _applicationTimesToday = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, TimeSpan> _activityTimesToday = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _activeApplicationsToday = new(StringComparer.OrdinalIgnoreCase);
        private readonly EventHandler<ActivityReadEventArgs> _activityReadHandler;
        private bool _isDisposed;
        private DateTime _currentStatsDate = DateTime.Now.Date;

        private string _allActivities = string.Empty;
        public string AllActivities
        {
            get { return _allActivities; }
            set { SetProperty(ref _allActivities, value); }
        }

        private string _currentActivityDisplay = "Waiting for captured activity...";
        public string CurrentActivityDisplay
        {
            get { return _currentActivityDisplay; }
            set { SetProperty(ref _currentActivityDisplay, value); }
        }

        private ObservableCollection<ActivityViewModel> _activities = [];
        public ObservableCollection<ActivityViewModel> Activities
        {
            get { return _activities; }
            set { _activities = value; }
        }

        private ObservableCollection<ActivityFlatViewModel> _flatActivities = [];
        public ObservableCollection<ActivityFlatViewModel> FlatActivities
        {
            get { return _flatActivities; }
            set { _flatActivities = value; }
        }

        private ObservableCollection<TopApplicationViewModel> _topApplicationsByTimeToday = [];
        public ObservableCollection<TopApplicationViewModel> TopApplicationsByTimeToday
        {
            get { return _topApplicationsByTimeToday; }
            set { SetProperty(ref _topApplicationsByTimeToday, value); }
        }

        private ObservableCollection<TopActivityViewModel> _topActivitiesByTimeToday = [];
        public ObservableCollection<TopActivityViewModel> TopActivitiesByTimeToday
        {
            get { return _topActivitiesByTimeToday; }
            set { SetProperty(ref _topActivitiesByTimeToday, value); }
        }

        private TimeSpan _totalTrackedTimeToday;
        public TimeSpan TotalTrackedTimeToday
        {
            get { return _totalTrackedTimeToday; }
            set { SetProperty(ref _totalTrackedTimeToday, value); }
        }

        private int _activeApplicationsCount;
        public int ActiveApplicationsCount
        {
            get { return _activeApplicationsCount; }
            set { SetProperty(ref _activeApplicationsCount, value); }
        }

        private TimeSpan _averageTimePerApplication;
        public TimeSpan AverageTimePerApplication
        {
            get { return _averageTimePerApplication; }
            set { SetProperty(ref _averageTimePerApplication, value); }
        }

        public ICommand SaveRawLogCommand { get; }

        public ActivitiesViewModel(ActivityMonitor activityMonitor, IRawActivitiesExportService rawActivitiesExportService)
        {
            _allActivitiesBuffer = new HashSet<string>();
            _activityMonitor = activityMonitor;
            _rawActivitiesExportService = rawActivitiesExportService;
            _activityReadHandler = OnActivityRead;
            SaveRawLogCommand = new RelayCommand(SaveRawLog);
            Activities = [];
            FlatActivities = [];
            TopApplicationsByTimeToday = [];
            TopActivitiesByTimeToday = [];
            //todo remove before pull request
            //GenerateMockTreeStructure();
            _activityMonitor.OnActivityRead += _activityReadHandler;
            if (Application.Current is not null)
            {
                Application.Current.Exit += OnApplicationExit;
            }
            _activityMonitor.Start();
        }

        private void OnActivityRead(object? s, ActivityReadEventArgs e)
        {
            if (_isDisposed || e.Activity is null)
                return;

            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher is null || dispatcher.HasShutdownStarted || dispatcher.HasShutdownFinished)
                return;

            if (dispatcher.CheckAccess())
            {
                UpdateActivityState(e.Activity, e.Time);
                return;
            }

            try
            {
                _ = dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    if (_isDisposed)
                        return;

                    UpdateActivityState(e.Activity, e.Time);
                }));
            }
            catch (TaskCanceledException)
            {
                // Dispatcher is shutting down; no UI update is needed.
            }
            catch (InvalidOperationException)
            {
                // Dispatcher is unavailable during shutdown; safely ignore.
            }
        }

        private void UpdateActivityState(UserActivity activity, TimeSpan activityTime)
        {
            CurrentActivityDisplay = BuildCurrentActivityDisplay(activity);

            var dataRow = $"{activity.Executable} | {activity.ApplicationName} | {activity.ActivityDescription} | {activity.InnerActivityDescription}";
            if (_allActivitiesBuffer.Add(dataRow))
            {
                AllActivities = string.Join(Environment.NewLine, _allActivitiesBuffer);
            }

            ResetDayScopedStatisticsIfNeeded();
            _hierarchyOperations.Upsert(Activities, activity, activityTime);
            AppendActivity(activity, activityTime);
            UpdateApplicationTime(activity.ApplicationName, activityTime);
            UpdateActivityTime(activity.ActivityDescription, activity.ApplicationName, activityTime);
            UpdateUsageStatistics(activity, activityTime);
            RefreshTopApplicationsByTimeToday();
            RefreshTopActivitiesByTimeToday();
        }

        private static string BuildCurrentActivityDisplay(UserActivity activity)
        {
            var appName = NormalizeApplicationName(activity.ApplicationName);
            var activityName = NormalizeActivityName(activity.ActivityDescription, activity.ApplicationName);
            var innerActivity = string.IsNullOrWhiteSpace(activity.InnerActivityDescription)
                ? "n/a"
                : activity.InnerActivityDescription;

            return $"{appName} | {activityName} | {innerActivity}";
        }

        private void OnApplicationExit(object? sender, ExitEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (Application.Current is not null)
            {
                Application.Current.Exit -= OnApplicationExit;
            }

            _activityMonitor.OnActivityRead -= _activityReadHandler;
            _activityMonitor.Stop();
        }

        private void SaveRawLog()
        {
            _rawActivitiesExportService.Export(AllActivities);
        }

        private void ResetDayScopedStatisticsIfNeeded()
        {
            var currentDate = DateTime.Now.Date;
            if (currentDate == _currentStatsDate)
                return;

            _currentStatsDate = currentDate;
            _applicationTimesToday.Clear();
            _activityTimesToday.Clear();
            _activeApplicationsToday.Clear();
            TotalTrackedTimeToday = TimeSpan.Zero;
            ActiveApplicationsCount = 0;
            AverageTimePerApplication = TimeSpan.Zero;
            TopApplicationsByTimeToday.Clear();
            TopActivitiesByTimeToday.Clear();
        }

        private void UpdateUsageStatistics(UserActivity userActivity, TimeSpan activityTime)
        {
            var applicationName = NormalizeApplicationName(userActivity.ApplicationName);

            TotalTrackedTimeToday += activityTime;
            _activeApplicationsToday.Add(applicationName);
            ActiveApplicationsCount = _activeApplicationsToday.Count;

            AverageTimePerApplication = ActiveApplicationsCount <= 0
                ? TimeSpan.Zero
                : TimeSpan.FromTicks(TotalTrackedTimeToday.Ticks / ActiveApplicationsCount);
        }

        private static string NormalizeApplicationName(string? applicationName)
        {
            return string.IsNullOrWhiteSpace(applicationName) ? "Unknown" : applicationName;
        }

        private static string NormalizeActivityName(string? activityName, string? applicationName)
        {
            if (!string.IsNullOrWhiteSpace(activityName))
                return activityName;

            return NormalizeApplicationName(applicationName);
        }

        private void UpdateApplicationTime(string? applicationName, TimeSpan activityTime)
        {
            var name = NormalizeApplicationName(applicationName);
            if (_applicationTimesToday.TryGetValue(name, out var currentTime))
            {
                _applicationTimesToday[name] = currentTime + activityTime;
                return;
            }

            _applicationTimesToday[name] = activityTime;
        }

        private void UpdateActivityTime(string? activityName, string? applicationName, TimeSpan activityTime)
        {
            var name = NormalizeActivityName(activityName, applicationName);
            if (_activityTimesToday.TryGetValue(name, out var currentTime))
            {
                _activityTimesToday[name] = currentTime + activityTime;
                return;
            }

            _activityTimesToday[name] = activityTime;
        }

        private void RefreshTopApplicationsByTimeToday()
        {
            var topThree = _applicationTimesToday
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(3)
                .ToList();

            TopApplicationsByTimeToday.Clear();

            for (var i = 0; i < topThree.Count; i++)
            {
                TopApplicationsByTimeToday.Add(new TopApplicationViewModel
                {
                    Rank = i + 1,
                    ApplicationName = topThree[i].Key,
                    TimeSpent = topThree[i].Value,
                });
            }
        }

        private void RefreshTopActivitiesByTimeToday()
        {
            var topActivities = _activityTimesToday
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(5)
                .ToList();

            TopActivitiesByTimeToday.Clear();

            for (var i = 0; i < topActivities.Count; i++)
            {
                TopActivitiesByTimeToday.Add(new TopActivityViewModel
                {
                    Rank = i + 1,
                    ActivityName = topActivities[i].Key,
                    TimeSpent = topActivities[i].Value,
                });
            }
        }

        private void AppendActivity(UserActivity userActivity, TimeSpan activityTime)
        {
            var existingActivity = FlatActivities.FirstOrDefault(x => x.ActivityDescription == userActivity.ActivityDescription 
                                                && x.ApplicationName == userActivity.ApplicationName
                                                && x.InnerActivityDescription == userActivity.InnerActivityDescription);
            if(existingActivity is not null)
            {
                existingActivity.ActivityTime += activityTime;
                return;
            }

            FlatActivities.Add(new ActivityFlatViewModel 
            { 
                ApplicationName = userActivity.ApplicationName,
                ActivityDescription = userActivity.ActivityDescription,
                InnerActivityDescription = userActivity.InnerActivityDescription,
                ActivityTime = activityTime
            });
        }

        //todo remove before pull request
        private void GenerateMockTreeStructure()
        {
            Activities =
            [
                new ActivityViewModel
                {
                    Activity = "Opera",
                    Time = TimeSpan.FromMinutes(13),
                    Percentage = 23,
                    InnerActivities =
                    [
                        new ActivityViewModel
                        {
                            Activity = "Github",
                            Percentage = 3,
                            Time = TimeSpan.FromMinutes(12),
                        },
                        new ActivityViewModel
                        {
                            Activity = "Youtube",
                            Percentage = 2,
                            Time = TimeSpan.FromMinutes(3),
                        }
                    ]
                },
                new ActivityViewModel
                {
                    Activity = "notepad",
                    Time = TimeSpan.FromMinutes(3),
                    Percentage = 8,
                    InnerActivities =
                    [
                        new ActivityViewModel
                        {
                            Activity = "Lorem ipsum",
                            Percentage = 2,
                            Time = TimeSpan.FromMinutes(3),
                        }
                    ]
                },
            ];
        }
    }
}
