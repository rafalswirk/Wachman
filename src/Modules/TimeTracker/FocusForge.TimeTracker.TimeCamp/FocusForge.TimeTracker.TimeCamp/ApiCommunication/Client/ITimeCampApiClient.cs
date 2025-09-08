using RestSharp;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client
{
    public interface ITimeCampApiClient
    {
        IRestClient Client { get; }
        void Initialize(string apiKey);
    }
}