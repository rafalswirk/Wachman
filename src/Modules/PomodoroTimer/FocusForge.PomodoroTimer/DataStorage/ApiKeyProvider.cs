using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.Utils.DataStorage
{
    class ApiKeyProvider : IApiKeyProvider
    {
        public string GetKey()
        {
            //return Properties.Settings.Default.ApiKey;
            return "";
        }

        public void SetKey(string key)
        {
            //Properties.Settings.Default.ApiKey = key;
            //Properties.Settings.Default.Save();
        }
    }
}
