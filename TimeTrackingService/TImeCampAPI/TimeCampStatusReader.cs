using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeTrackingService.TimeCampAPI
{
    public class TimeCampStatusReader
    {
        private readonly string apiKey;
        private RestClient client;
        public TimeCampStatusReader(string apiKey)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.apiKey = apiKey;
            client = new RestClient(@"https://www.timecamp.com/third_party/api");
        }

        public string GetCurrentJob()
        {

            try
            {
                var request = new RestRequest
                {
                    Resource = $"/timer/api_token/{apiKey}",
                    Method = Method.Get
                };
                request.AddParameter("action", "status", ParameterType.GetOrPost);
                var response = client.PostAsync(request);
                response.Wait();
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response.Result.Content);
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
