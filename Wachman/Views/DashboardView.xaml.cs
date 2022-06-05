using DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeTrackingService.DummyAPI;
using TimeTrackingService.TimeCampAPI;
using TimeTrackingService.TimeCampAPI.Client;
using Wachman.Utils.DataStorage;
using Wachman.ViewModels;

namespace Wachman.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        private DashboardViewModel _dataContext;

        public DashboardView()
        {
            InitializeComponent();
            bool useDummyService = true;
            _dataContext = new DashboardViewModel(useDummyService ?
                new DummyTrackingService()
                : new TimeCampService(new ApiKeyProvider().GetKey()));
            DataContext = _dataContext;      
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _dataContext.OnLoaded();
        }
    }
}
