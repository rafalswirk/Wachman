using System;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using RestSharp;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication;

public class GetAvailableTasks
{
    private readonly RestClient _apiClient;

    public GetAvailableTasks(RestClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<TaskInfoDTO>> GetTasksAsync()
    {
        var result = new List<TaskInfoDTO>();

        var request = new RestRequest
        {
            Resource = $"/tasks",
            Method = Method.Get
        };

        request.AddParameter("exclude_archived", 1);
        request.AddParameter("minimal", 1);

        var response = await _apiClient.ExecuteAsync<Dictionary<string, TaskInfoDTO>>(request);

        if(response.IsSuccessful && response.Data != null)
        {
            result = response.Data.Select(x => new TaskInfoDTO(x.Value.task_id, x.Value.name)).ToList();
        }
        else
        {
            throw new Exception($"Failed to retrieve tasks: {response.ErrorMessage}");
        }

        return result;
    }
}
