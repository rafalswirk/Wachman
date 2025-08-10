using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.UI.Models;
using FocusForge.UI.Dialogs;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class SyncTasksViewModel: ObservableObject
    {
        private readonly ITasksLoader _tasksLoader;
        private readonly ITasksWriter _tasksWriter;
        private readonly ITasksReader _tasksReader;
        private readonly IDialog _dialog;

        public IAsyncRelayCommand LoadTasksCommand { get; set; }
        public RelayCommand ImportSelected { get; set; }

        private List<SyncTaskItem> _tasks;

        public List<SyncTaskItem> Tasks
        {
            get { return _tasks; }
            set { SetProperty(ref _tasks, value); }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }


        public SyncTasksViewModel(ITasksLoader tasksLoader, ITasksWriter tasksWriter, ITasksReader tasksReader, IDialog dialog)
        {
            _tasksLoader = tasksLoader;
            _tasksWriter = tasksWriter;
            _tasksReader = tasksReader;
            _dialog = dialog;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoadTasksCommand = new AsyncRelayCommand(async () => 
            {
                var tasks = await _tasksLoader.LoadTasksAsync();
                var syncedTasks = await _tasksReader.ReadAsync();
                Tasks = tasks.Select(t => new SyncTaskItem { Task = t, Import = syncedTasks.Any(s => s.ExternalId == t.ExternalId) }).ToList();
                IsExpanded = true;
            });

            ImportSelected = new RelayCommand(async () =>
            {
                var selectedTasks = Tasks.Where(t => t.Import).Select(t => t.Task).ToList();
                if (selectedTasks.Any())
                {
                    await _tasksWriter.SaveTasksAsync(selectedTasks);
                    _dialog.Show(new DialogData("Import completed"));
                }
            });
        }
    }
}
