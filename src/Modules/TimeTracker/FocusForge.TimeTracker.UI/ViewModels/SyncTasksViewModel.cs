using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.UI.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class SyncTasksViewModel: ObservableObject
    {
        private readonly ITasksLoader _tasksLoader;
        private readonly ITasksWriter _tasksWriter;

        public IAsyncRelayCommand LoadTasksCommand { get; set; }
        public RelayCommand ImportSelected { get; set; }
        private List<SyncTaskItem> _tasks;

        public List<SyncTaskItem> Tasks
        {
            get { return _tasks; }
            set { SetProperty(ref _tasks, value); }
        }


        public SyncTasksViewModel(ITasksLoader tasksLoader, ITasksWriter tasksWriter)
        {
            InitializeCommands();
            _tasksLoader = tasksLoader;
            _tasksWriter = tasksWriter;
        }

        private void InitializeCommands()
        {
            LoadTasksCommand = new AsyncRelayCommand(async () => 
            {
                var tasks = await _tasksLoader.LoadTasksAsync();
                Tasks = tasks.Select(t => new SyncTaskItem { Task = t, Import = false }).ToList();
            });
        }
    }
}
