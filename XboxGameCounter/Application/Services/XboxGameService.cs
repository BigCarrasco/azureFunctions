using Company.Function.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace Company.Function.Application.Services;
public class XboxGameService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<XboxGameService> _logger;

    public XboxGameService(HttpClient httpClient, ILogger<XboxGameService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<int> GetTotalGamesAsync()
    {
        var apiUrl = "https://api.sampleapis.com/xbox/games";
        var apiResponse = await _httpClient.GetAsync(apiUrl); //Object Construction

        if (!apiResponse.IsSuccessStatusCode)
        {
            _logger.LogError("API FAILED WITH STATUS {statuscode}", apiResponse.StatusCode);
            return 0;
        }

        var content = await apiResponse.Content.ReadAsStringAsync();
        var games = JsonSerializer.Deserialize<List<Game>>(content);

        return games?.Count ?? 0; // Null operator
    }
}