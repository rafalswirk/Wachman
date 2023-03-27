using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.ViewModels
{
    public class SettingsViewModel: ObservableObject 
    {
        public string ApiKey { get; set; }
        public bool TimeCampIntegrationEnabled { get; set; }
    }
}
