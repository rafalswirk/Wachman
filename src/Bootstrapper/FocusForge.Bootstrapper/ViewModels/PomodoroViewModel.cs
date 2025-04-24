using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wachman.CustomEventArgs;
using Wachman.DAL;
using Wachman.DAL.Respositories;
using Wachman.Models;
using Wachman.Repositories;
using Wachman.Views;
using Wachman.Windows;

namespace Wachman.ViewModels
{
    public class PomodoroViewModel : ObservableObject
    {
        private MicroTimerView _timerDialog;

        private int _numberOfWorkingSessions;
        private readonly IConfigurationRepository _configurationRepository;

        public int NumberOfWorkingSessions
        {
            get => _numberOfWorkingSessions;
            set => SetProperty(ref _numberOfWorkingSessions, value);
        }


        public int WorkSessionDuration { get; set; } = 30;
        public int BreakTimeDuration { get; set; } = 5;
        public ICommand RunTimer { get; set; }

        public PomodoroViewModel(IConfigurationRepository configurationRepository)
        {
            LoadSettings();
            NumberOfWorkingSessions = 0;
            RunTimer = new RelayCommand(() => 
            {
                SaveSettings(new PomodoroConfiguration()
                {
                    WorkSessionDuration = WorkSessionDuration,
                    BreakTimeDuration = BreakTimeDuration,
                    //DisableBreaks = DisableBreaks
                });
                if(_timerDialog is not null)
                {
                    _timerDialog.OnTimerFinished -= _dialog_OnTimerFinished;
                }
                _timerDialog = new MicroTimerView(WorkSessionDuration);
                _timerDialog.OnTimerFinished += _dialog_OnTimerFinished;
                _timerDialog.Show();
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });

            LoadSettings();
            _configurationRepository = configurationRepository;
        }

        private void LoadSettings()
        {
            var configuration = _configurationRepository.GetConfigurationAsync().Result;
            if (configuration != null)
            {
                WorkSessionDuration = configuration.WorkSessionDuration;
                BreakTimeDuration = configuration.BreakTimeDuration;
                //DisableBreaks = configuration.DisableBreaks;
            }
        }

        public void SaveSettings(PomodoroConfiguration configuration)
        {
            _configurationRepository.SaveConfigurationAsync(configuration).Wait();
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
