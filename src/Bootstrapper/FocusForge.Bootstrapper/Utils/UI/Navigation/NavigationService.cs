using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Desktop.Utils.UI.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private IServiceProvider _serviceProvider;
        private ObservableObject _viewModel;

        public NavigationService()
        {   
            
        }

        public ObservableObject ViewModel 
        {
            get => _viewModel;
            private set => SetProperty(ref _viewModel, value); 
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ObservableObject
        {
            ViewModel = _serviceProvider.GetRequiredService<TViewModel>();   
        }
    }
}
