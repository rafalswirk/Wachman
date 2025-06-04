using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class SyncTasksViewModel
    {
        public RelayCommand LoadTasksCommand { get; set; }
        public RelayCommand ImportSelected { get; set; }

        public SyncTasksViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            throw new NotImplementedException();
        }
    }
}
