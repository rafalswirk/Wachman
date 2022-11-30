using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Wachman.ViewModels
{
    public class MicroBreakViewModel: ObservableObject
    {
        private Timer _timer = new();

        private bool _isMessageVisible;
        private DateTime _startTime;
        private TimeSpan breakTime = TimeSpan.FromMinutes(5);

        public bool IsBreakModeEnabled
        {
            get => _isMessageVisible;
            set => SetProperty(ref _isMessageVisible, value);
        }

        private string _userMessage;
        public string UserMessage
        {
            get => _userMessage;
            set => SetProperty(ref _userMessage, value);
        }

        private int _breakProgress;

        public int BreakProgress
        {
            get => _breakProgress;
            set => SetProperty(ref _breakProgress, value);
        }



        public ICommand TakeBreak { get; set; }
        public ICommand SkipBreak { get; set; }
        public ICommand PostponeBreak { get; set; }

        public event EventHandler OnBreakSkipped;
        public event EventHandler OnBreakPostponed;

        public MicroBreakViewModel()
        {
            UserMessage = "You can take 5 minutes break";
            TakeBreak = new RelayCommand(() => 
            {
                IsBreakModeEnabled = true;
                BreakProgress = 100;
                UserMessage = $"{breakTime.Minutes:00}:{breakTime.Seconds:00}";
                _startTime = DateTime.Now;
                _timer.Interval = 1000;
                _timer.Elapsed += (o, e) => 
                {
                    var elpassedTime = DateTime.Now - _startTime;
                    var timeToFinish = breakTime - elpassedTime;
                    UserMessage = $"{timeToFinish.Minutes:00}:{timeToFinish.Seconds:00}";
                    BreakProgress = (int)(100 - (elpassedTime.TotalSeconds * 100) / breakTime.TotalSeconds);
                };
                _timer.Start();
            });

            SkipBreak = new RelayCommand(() => 
            {
                OnBreakSkipped?.Invoke(this, EventArgs.Empty);
            });

            PostponeBreak = new RelayCommand(() => 
            {
                OnBreakPostponed?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
