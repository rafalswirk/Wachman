using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wachman.Views;

namespace Wachman.ViewModels
{
    public class PomodoroViewModel : ObservableObject
    {
        public int WorkSessionDuration { get; set; } = 30;
        public ICommand RunTimer { get; set; }

        public PomodoroViewModel()
        {
            RunTimer = new RelayCommand(() => 
            {
                var dialog = new MicroTimerView(WorkSessionDuration);
                dialog.Show();
            });
        }
    }
}
