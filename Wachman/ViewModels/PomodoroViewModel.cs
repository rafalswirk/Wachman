﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wachman.CustomEventArgs;
using Wachman.Views;
using Wachman.Windows;

namespace Wachman.ViewModels
{
    public class PomodoroViewModel : ObservableObject
    {
        private MicroTimerView _timerDialog;

        private int _numberOfWorkingSessions;

        public int NumberOfWorkingSessions
        {
            get => _numberOfWorkingSessions;
            set => SetProperty(ref _numberOfWorkingSessions, value);
        }


        public int WorkSessionDuration { get; set; } = 30;
        public int BreakTimeDuration { get; set; } = 5;
        public ICommand RunTimer { get; set; }

        public PomodoroViewModel()
        {
            NumberOfWorkingSessions = 0;
            RunTimer = new RelayCommand(() => 
            {
                if(_timerDialog is not null)
                {
                    _timerDialog.OnTimerFinished -= _dialog_OnTimerFinished;
                }
                _timerDialog = new MicroTimerView(WorkSessionDuration);
                _timerDialog.OnTimerFinished += _dialog_OnTimerFinished;
                _timerDialog.Show();
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
        }

        private void _dialog_OnTimerFinished(object sender, OnSessionFinishedEventArgs e)
        {
            if (e.InterruptedByUser)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                return;
            }

            _timerDialog.WindowState = WindowState.Minimized;
            NumberOfWorkingSessions++;
            var dialog = new MicroBreakWindow();
            var breakViewModel = new MicroBreakViewModel(TimeSpan.FromMinutes(BreakTimeDuration));
            breakViewModel.OnBreakFinished += (o, e) =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => 
                {
                    _timerDialog.Close();
                    dialog.Close();
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }));
            };
            breakViewModel.OnBreakPostponed += (o, e) =>
            {
                NumberOfWorkingSessions--;
                _timerDialog.RunTimer(5);
                _timerDialog.WindowState = WindowState.Normal;
                dialog.Close();
            };
            dialog.DataContext = breakViewModel;
            dialog.Show();
        }
    }
}
