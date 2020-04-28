using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.Utils.TimeCamp
{
    class TimeCampStatusReader
    {
        private readonly string apiKey;

        public TimeCampStatusReader(string apiKey)
        {
            this.apiKey = apiKey;
        }

        internal string GetCurrentJob()
        {
            var client = new RestClient(@"https://www.timecamp.com/third_party/api");
            var request = new RestRequest(Method.GET);
            throw new NotImplementedException();
        }
    }
}
