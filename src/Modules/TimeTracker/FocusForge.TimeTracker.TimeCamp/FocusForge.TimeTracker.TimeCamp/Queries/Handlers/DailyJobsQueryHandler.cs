using FocusForge.DataModels.Jobs;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FocusForge.TimeTracker.TimeCamp.Queries.Handlers
{
    public class DailyJobsQueryHandler : ITimeTrackingCommand<List<Job>?>
    {
        private readonly ITimeCampApiClient _apiClient;

        public DailyJobsQueryHandler(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<List<Job>?> ExecuteAsync()
        {
            try
            {
                var request = new RestRequest
                {
                    Resource = $"/entries",
                    Method = Method.Get
                };
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                request.AddParameter("from", today);
                request.AddParameter("to", today);
                var response = await _apiClient.Client.ExecuteAsync<IEnumerable<EntriesDTO>>(request);
                if (!response.IsSuccessful)
                    return null;
                if (response.Data is null)
                    return new List<Job>();
                var result = new List<Job>();
                foreach (var dto in response.Data)
                {
                    var job = new Job
                    {
                        Description = dto.Description,
                        Name = dto.Name,
                        Start = DateTime.ParseExact($"{dto.Date.ToString("yyyy-MM-dd")} {dto.Start_Time}", "yyyy-MM-dd HH:mm:ss", null),
                        Stop = DateTime.ParseExact($"{dto.Date.ToString("yyyy-MM-dd")} {dto.End_Time}", "yyyy-MM-dd HH:mm:ss", null)
                    };
                    job.Duration = job.Stop - job.Start;
                    if (dto == response.Data.Last() && job.Duration == TimeSpan.Zero)
                        job.IsRunning = true;


                    result.Add(job);
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
