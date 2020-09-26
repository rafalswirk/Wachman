using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wachman.Utils.TimeCamp
{
    class TimeCampStatusReader
    {
        private readonly string apiKey;
        private RestClient client;
        public TimeCampStatusReader(string apiKey)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.apiKey = apiKey;
            client = new RestClient(@"https://www.timecamp.com/third_party/api");
        }

        internal string GetCurrentJob()
        {

            try
            {
                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/timer/api_token/{apiKey}"
                };
                request.AddParameter("action", "status", ParameterType.GetOrPost);
                var response = client.Post(request);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response.Content);
                var status = xmldoc.GetElementsByTagName("name");

                return status[0].InnerText;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
