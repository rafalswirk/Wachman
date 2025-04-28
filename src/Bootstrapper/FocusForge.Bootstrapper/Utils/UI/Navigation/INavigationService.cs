using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Desktop.Utils.UI.Navigation
{
    public interface INavigationService
    {
        ObservableObject ViewModel { get; }
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ObservableObject;
    }
}
