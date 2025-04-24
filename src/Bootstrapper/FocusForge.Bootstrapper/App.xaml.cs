using FocusForge.Desktop.Utils;
using FocusForge.Desktop.Utils.UI.Navigation;
using FocusForge.Desktop.ViewModels;
using FocusForge.PomodoroTimer.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeTrackingService;
using TimeTrackingService.DummyAPI;
using Wachman.DAL;
using Wachman.DAL.Respositories;
using Wachman.Repositories;
using Wachman.Utils.DataStorage;
using Wachman.ViewModels;

namespace Wachman
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
                    services.AddSingleton<CurrentDayViewModel>();
                    services.AddSingleton<SettingsViewModel>();
                    services.AddSingleton<ITimeTrackingService, DummyTrackingService>();
                    services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
                    services.AddScoped<WachmanDbContext>();
                })
                .Build();

            AutomatedMigrations.Apply();
            _host.ConfigureNavigationService();
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
