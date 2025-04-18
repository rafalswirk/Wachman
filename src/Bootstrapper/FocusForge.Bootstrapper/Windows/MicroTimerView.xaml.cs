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
using System.Windows.Threading;
using TimeTrackingService.TimeCampAPI;
using Wachman.Utils.DataStorage;
using Wachman.Utils;
using System.Threading;
using Wachman.CustomEventArgs;

namespace Wachman.Views
{
    /// <summary>
    /// Interaction logic for MicroTimerView.xaml
    /// </summary>
    public partial class MicroTimerView : Window
    {
        private TimeCampStatusReader timeCampStatusReader;
        DispatcherTimer timer;
        DateTime startTime;
        TimeSpan workingTime = TimeSpan.FromMinutes(25);
        int complitedWorkingSessions;
        private double lastTop;
        private double lastLeft;
        private DateTime pauseTime;
        private CancellationTokenSource cancellationTokenSource;

        public event EventHandler<OnSessionFinishedEventArgs> OnTimerFinished;

        public MicroTimerView(int minutes)
        {
            InitializeComponent();

            RunTimer(minutes);
        }

        public void RunTimer(int minutes)
        {
            if (minutes > 99)
                throw new ArgumentException("Number of minutes cannot be greater than 99");

            workingTime = TimeSpan.FromMinutes(minutes);
            lblTime.Content = $"{minutes}:00";
            timeCampStatusReader = new TimeCampStatusReader(new ApiKeyProvider().GetKey());

            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (o, e) =>
            {
                UpdateClock();
            };

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(3000);
                    if(cancellationTokenSource.IsCancellationRequested) 
                        return;
                    var activeTask = await timeCampStatusReader.GetCurrentJobAsync();
                    await lblActivity?.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        if(lblActivity != null)
                            lblActivity.Content = activeTask;
                    }));
                }
            }, cancellationTokenSource.Token);

            StartTimer();
        }

        private void UpdateClock()
        {
            var ellpasedTime = workingTime - (DateTime.Now - startTime);
            if (ellpasedTime.TotalSeconds < 0)
            {
                complitedWorkingSessions++;
                timer.Stop();
                btnStart.Visibility = Visibility.Visible;
                OnTimerFinished?.Invoke(this, new OnSessionFinishedEventArgs {InterruptedByUser = false });
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
            StartTimer();
        }

        private void StartTimer()
        {
            startTime = DateTime.Now;
            timer.Start();
            btnStart.Visibility = Visibility.Hidden;
            lblActivity.Visibility = Visibility.Visible;
            UpdateClock();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            AeroGlassHelper.EnableBlur(this, true);
            stackPanelControl.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            AeroGlassHelper.EnableBlur(this, false);
            stackPanelControl.Visibility = Visibility.Collapsed;
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

        private void AcrylicWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlassHelper.EnableBlur(this, false);
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            btnPause.Visibility = Visibility.Collapsed;
            btnResume.Visibility = Visibility.Visible;
            pauseTime = DateTime.Now;
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            startTime = startTime + (DateTime.Now - pauseTime);
            btnPause.Visibility = Visibility.Visible;
            btnResume.Visibility = Visibility.Collapsed;
            timer.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Close();
            OnTimerFinished?.Invoke(this, new OnSessionFinishedEventArgs { InterruptedByUser = true });
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
