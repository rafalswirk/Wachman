using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication
{
    public class TimeCampStatusReader
    {
        private readonly ITimeCampApiClient _apiClient;

        public TimeCampStatusReader(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> GetCurrentJobAsync()
        {

            try
            {
                var request = new RestRequest
                {
                    Resource = $"/timer_running",
                    Method = Method.Get
                };
                var response = await _apiClient.Client.PostAsync(request);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response.Content);
                var status = xmldoc.GetElementsByTagName("name");
                if (status.Count == 0)
                    return string.Empty;
                return status[0].InnerText;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
