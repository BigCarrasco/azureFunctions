using Company.Function.Models;
using System.Net.Http;
using System.Text.Json;

namespace Company.Function.Services;
public class XboxGameService
{
    private readonly HttpClient _httpClient;

    public XboxGameService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<int> GetTotalGamesAsync()
    {
        var apiUrl = "https://api.sampleapis.com/xbox/games";
        var apiResponse = await _httpClient.GetAsync(apiUrl);

        if (!apiResponse.IsSuccessStatusCode)
        {
            return 0; //agrregar más
        }

        var content = await apiResponse.Content.ReadAsStringAsync();
        var games = JsonSerializer.Deserialize<List<Game>>(content);

        return games?.Count ?? 0;
    }
}
