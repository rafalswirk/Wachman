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
        private TimeSpan _breakTime = TimeSpan.FromMinutes(5);

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

        public event EventHandler OnBreakFinished;
        public event EventHandler OnBreakPostponed;

        public MicroBreakViewModel(TimeSpan breakTime)
        {
            _breakTime = breakTime;
            UserMessage = $"You can take {_breakTime.Minutes} minutes break";
            TakeBreak = new RelayCommand(() => 
            {
                IsBreakModeEnabled = true;
                BreakProgress = 100;
                UserMessage = $"{_breakTime.Minutes:00}:{_breakTime.Seconds:00}";
                _startTime = DateTime.Now;
                _timer.Interval = 1000;
                _timer.Elapsed += (o, e) => 
                {
                    var elpassedTime = DateTime.Now - _startTime;
                    var timeToFinish = _breakTime - elpassedTime;
                    UserMessage = $"{timeToFinish.Minutes:00}:{timeToFinish.Seconds:00}";
                    BreakProgress = (int)(100 - (elpassedTime.TotalSeconds * 100) / _breakTime.TotalSeconds);
                    if (timeToFinish <= TimeSpan.Zero)
                    {
                        var timer = o as Timer;
                        timer.Stop();
                        timer.Dispose();
                        UserMessage = $"Get back to work!!!";
                        OnBreakFinished?.Invoke(this, EventArgs.Empty);
                    }
                };
                _timer.Start();
            });

            SkipBreak = new RelayCommand(() => 
            {
                OnBreakFinished?.Invoke(this, EventArgs.Empty);
            });

            PostponeBreak = new RelayCommand(() => 
            {
                OnBreakPostponed?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
