using CarMeetUpApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarMeetUpApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    // settings configured in order to denote that we need an API key in order to access our external api and the naming convention that went into user secrets
    public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        var apiKey = configuration["ApiKeys:CarApiKey"];
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    }

   
    public async Task<List<Car>> GetCarDataAsync(string make)
    {
        var response = await _httpClient.GetAsync($"https://api.api-ninjas.com/v1/cars?make={make}");

      
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            // this was neeeded since the api we are bringing in shows jsons as the result
            return JsonConvert.DeserializeObject<List<Car>>(responseData);
        }
        else
        {
            
            throw new HttpRequestException($"Error retrieving car data: {response.StatusCode}");
        }
    }
}
