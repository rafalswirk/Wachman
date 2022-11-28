using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wachman.Views;

namespace Wachman.ViewModels
{
    public class PomodoroViewModel : ObservableObject
    {
        private MicroTimerView _dialog;

        private int _numberOfWorkingSessions;

        public int NumberOfWorkingSessions
        {
            get => _numberOfWorkingSessions;
            set => SetProperty(ref _numberOfWorkingSessions, value);
        }


        public int WorkSessionDuration { get; set; } = 30;
        public ICommand RunTimer { get; set; }

        public PomodoroViewModel()
        {
            NumberOfWorkingSessions = 0;
            RunTimer = new RelayCommand(() => 
            {
                if(_dialog is not null)
                {
                    _dialog.OnTimerFinished -= _dialog_OnTimerFinished;
                }
                _dialog = new MicroTimerView(WorkSessionDuration);
                _dialog.OnTimerFinished += _dialog_OnTimerFinished;
                _dialog.Show();
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
        }

        private void _dialog_OnTimerFinished(object sender, EventArgs e)
        {
            NumberOfWorkingSessions++;
        }
    }
}
