using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wachman.Utils.TimeCamp;

namespace Wachman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeCampStatusReader timeCampStatusReader;
        DispatcherTimer timer;
        DateTime startTime;
        TimeSpan workingTime = TimeSpan.FromMinutes(25);
        int complitedWorkingSessions;
        private double lastTop;
        private double lastLeft;

        public MainWindow()
        {
            InitializeComponent();

            timeCampStatusReader = new TimeCampStatusReader(new ApiKeyProvider().GetKey());

            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (o, e) =>
            {
                UpdateClock();
            };

            Task.Run(async () => 
            {
                while(true)
                {
                    await Task.Delay(3000);
                    var activeTask = timeCampStatusReader.GetCurrentJob();
                    await lblActivity.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action( ()=> 
                    {
                        lblActivity.Content = activeTask;
                    }));
                }
            });
        }

        private void UpdateClock()
        {
            var ellpasedTime = workingTime - (DateTime.Now - startTime);
            if (ellpasedTime.TotalSeconds < 0)
            {
                complitedWorkingSessions++;
                MessageBox.Show($"Przerwa. Ukończone sesje: {complitedWorkingSessions}.");
                timer.Stop();
                btnStart.Visibility = Visibility.Visible;
            }

            lblTime.Content = $"{ellpasedTime.Minutes:00}:{ellpasedTime.Seconds:00}";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            timer.Start();
            btnStart.Visibility = Visibility.Hidden;
            lblActivity.Visibility = Visibility.Visible;
            UpdateClock();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Brushes.White;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = Brushes.Transparent;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                lastTop = Top;
                lastLeft = Left;
            }

            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                Top = lastTop;
                Left = lastLeft;
            }
        }
    }
}
