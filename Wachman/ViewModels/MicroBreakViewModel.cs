using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wachman.ViewModels
{
    public class MicroBreakViewModel: ObservableObject
    {
        private bool _isMessageVisible;
        public bool IsMessageVisible
        {
            get => _isMessageVisible;
            set => SetProperty(ref _isMessageVisible, value);
        }

        public ICommand TakeBreak { get; set; }
        public ICommand SkipBreak { get; set; }
        public ICommand PostponeBreak { get; set; }

        public event EventHandler OnBreakSkipped;
        public event EventHandler OnBreakPostponed;

        public MicroBreakViewModel()
        {
            IsMessageVisible = true;
            TakeBreak = new RelayCommand(() => 
            {
                IsMessageVisible = false;
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
