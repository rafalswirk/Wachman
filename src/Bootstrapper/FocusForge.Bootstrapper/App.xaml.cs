using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wachman.DAL;

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
                })
                .Build();

            AutomatedMigrations.Apply();
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
