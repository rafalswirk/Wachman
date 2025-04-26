using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.PomodoroTimer.CustomEventArgs;
using FocusForge.PomodoroTimer.DataStorage;
using FocusForge.PomodoroTimer.Models;
using FocusForge.PomodoroTimer.Repositories;
using FocusForge.PomodoroTimer.UI.Views;
using FocusForge.PomodoroTimer.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FocusForge.PomodoroTimer.UI.ViewModels
{
    public class PomodoroViewModel : ObservableObject
    {
        private MicroTimerView _timerDialog;

        private int _numberOfWorkingSessions;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IApiKeyProvider _keyProvider;

        public int NumberOfWorkingSessions
        {
            get => _numberOfWorkingSessions;
            set => SetProperty(ref _numberOfWorkingSessions, value);
        }


        public int WorkSessionDuration { get; set; } = 30;
        public int BreakTimeDuration { get; set; } = 5;
        public ICommand RunTimer { get; set; }

        public PomodoroViewModel(IConfigurationRepository configurationRepository, IApiKeyProvider keyProvider)
        {
            _configurationRepository = configurationRepository;
            _keyProvider = keyProvider;
            LoadSettings();
            NumberOfWorkingSessions = 0;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            RunTimer = new RelayCommand(() =>
            {
                SaveSettings(new PomodoroConfiguration()
                {
                    WorkSessionDuration = WorkSessionDuration,
                    BreakTimeDuration = BreakTimeDuration,
                    //DisableBreaks = DisableBreaks
                });
                if (_timerDialog is not null)
                {
                    _timerDialog.OnTimerFinished -= _dialog_OnTimerFinished;
                }
                _timerDialog = new MicroTimerView(WorkSessionDuration, _keyProvider);
                _timerDialog.OnTimerFinished += _dialog_OnTimerFinished;
                _timerDialog.Show();
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
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
