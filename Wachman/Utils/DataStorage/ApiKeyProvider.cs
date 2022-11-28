using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wachman.Windows;

namespace Wachman.Utils.DataStorage
{
    class ApiKeyProvider : IApiKeyProvider
    {
        public string GetKey()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ApiKey))
            {
                var form = new AskForKeyWindow();
                if (form.ShowDialog() != true)
                    return "";
                Properties.Settings.Default.ApiKey = form.ApiKey;
                Properties.Settings.Default.Save();
            }

            return Properties.Settings.Default.ApiKey;
        }
    }
}
