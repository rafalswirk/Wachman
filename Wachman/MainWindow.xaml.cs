using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using Wachman.Utils.TimeCamp;

namespace Wachman
{
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

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

        internal void EnableBlur(bool enabled)
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = enabled ? AccentState.ACCENT_ENABLE_BLURBEHIND : AccentState.ACCENT_DISABLED;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
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
            EnableBlur(true);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            EnableBlur(false);
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
            EnableBlur(false);
        }
    }
}
