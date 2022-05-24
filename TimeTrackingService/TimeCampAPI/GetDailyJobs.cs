﻿using DataModels.Jobs;
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
using TimeTrackingService.TimeCampAPI.DTO;
using TimeTrackingService.TimeTrackingServiceCommand;

namespace TimeTrackingService.TimeCampAPI
{
    public class GetDailyJobs : ITimeTrackingCommand<List<Job>?>
    {
        private readonly RestClient _client;

        public GetDailyJobs(RestClient client)
        {
            _client = client;
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
                request.AddParameter("from", "2022-05-18");
                request.AddParameter("to", "2022-05-19");
                var response = await _client.ExecuteAsync<IEnumerable<EntriesDTO>>(request);
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
                        Start = DateTime.ParseExact(dto.Start_Time, "HH:mm:ss", null),
                        Stop = DateTime.ParseExact(dto.End_Time, "HH:mm:ss", null)
                    };
                    job.Duration = job.Stop - job.Start;
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
