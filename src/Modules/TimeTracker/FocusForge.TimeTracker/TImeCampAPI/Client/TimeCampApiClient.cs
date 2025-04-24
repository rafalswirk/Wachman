using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService.TimeCampAPI.Client
{
    public class TimeCampApiClient
    {
        private static RestClient? _instance;
        private static string? _apiKey;

        public static void Initialize(string apiKey)
        {
            _apiKey = apiKey;
        }
        public static RestClient Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _instance = new RestClient(@"https://app.timecamp.com/third_party/api");
                _instance.AddDefaultHeader("Accept", "application/json");
                _instance.AddDefaultHeader("Authorization", $"Bearer {_apiKey}");
                return _instance;
            }
        }
    }
}
