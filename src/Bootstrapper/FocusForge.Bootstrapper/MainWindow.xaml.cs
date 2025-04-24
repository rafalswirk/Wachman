using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using TimeTrackingService.TimeCampAPI;
using Wachman.Utils;
using Wachman.Utils.DataStorage;
using Wachman.ViewModels;

namespace Wachman
{
    


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(DashboardViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
