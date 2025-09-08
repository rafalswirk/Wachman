using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client
{
    public class TimeCampApiClient : ITimeCampApiClient
    {
        private RestClient _instance;

        public IRestClient Client
            => _instance;

        public void Initialize(string apiKey)
        {
            if (_instance != null)
            {
                return;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _instance = new RestClient(@"https://app.timecamp.com/third_party/api");
            _instance.AddDefaultHeader("Accept", "application/json");
            _instance.AddDefaultHeader("Authorization", $"Bearer {apiKey}");
        }
    }
}
