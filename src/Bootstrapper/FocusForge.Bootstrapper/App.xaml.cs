using FocusForge.Desktop.Utils;
using FocusForge.Desktop.Utils.UI.Navigation;
using FocusForge.Desktop.ViewModels;
using FocusForge.PomodoroTimer;
using FocusForge.PomodoroTimer.UI;
using FocusForge.TimeTracker;
using FocusForge.TimeTracker.DummyAPI;
using FocusForge.TimeTracker.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace FocusForge.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddPomodoroTimer();
                    services.AddTimeTracker();
                    services.AddSingleton<SettingsViewModel>();
                    services.AddSingleton<ITimeTrackingService, DummyTrackingService>();
                    services.AddSingleton<INavigationService, NavigationService>();
                })
                .Build();

            _host.ConfigureNavigationService();
            _host.ApplyMigrations();
            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if(_host != null)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
