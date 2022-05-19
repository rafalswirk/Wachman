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
using Wachman.Models.Jobs;

namespace Wachman.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            var dataItems = new List<Job>()
            {
                new Job{ Name = "ProgrammingKata", Description = "Making fancy algorithm", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(25)},
                new Job{ Name = "Mailing", Description = "Mail to office", Start = new DateTime(2021, 11, 1, 13, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(72)},
                new Job{ Name = "Tea time", Description = "Green tea", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromSeconds(320)}
            };

            dataGrid.ItemsSource = dataItems;
        }
    }
}
