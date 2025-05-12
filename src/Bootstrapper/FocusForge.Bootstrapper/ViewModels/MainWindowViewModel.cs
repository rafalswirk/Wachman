using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.DataModels.Jobs;
using FocusForge.Desktop.Utils.UI.Navigation;
using FocusForge.PomodoroTimer.DataStorage;
using FocusForge.PomodoroTimer.UI.ViewModels;
using FocusForge.TimeTracker;
using FocusForge.TimeTracker.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FocusForge.Desktop.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private ObservableObject _selectedViewModel;
        private readonly INavigationService _navigationService;

        public ObservableObject SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public INavigationService NavigationService => _navigationService;

        public ICommand ChangeJobStatus { get; private set; }
        public ICommand SwitchToCurrentDay { get; set; }
        public ICommand SwitchPomodoroTimer { get; set; }
        public ICommand SwitchToSettings { get; set; }
        public IAsyncRelayCommand OnLoadCommand { get; set; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            ChangeJobStatus = new RelayCommand<Job>(job =>
            {
                job.IsRunning = true;
            });
            SwitchToCurrentDay = new RelayCommand(() => _navigationService.NavigateTo<CurrentDayViewModel>());
            SwitchPomodoroTimer = new RelayCommand(() => _navigationService.NavigateTo<PomodoroViewModel>());
            SwitchToSettings = new RelayCommand(() => _navigationService.NavigateTo<SettingsViewModel>());
            _navigationService = navigationService;
        }
    }
}
