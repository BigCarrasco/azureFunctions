using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;

using Company.Function.Domain.Models;
using Company.Function.Application.Services;

namespace Company.Function.functions.Triggers;
public class XboxGameCounter
{
    private readonly ILogger<XboxGameCounter> _logger;
    private readonly XboxGameService _xboxGameService; // Inyectamos el servicio

    public XboxGameCounter(ILogger<XboxGameCounter> logger, XboxGameService xboxGameService)
    {
        _logger = logger;
        _xboxGameService = xboxGameService;
    }

    [Function("XboxGameCounter")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var totalGames = await _xboxGameService.GetTotalGamesAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new { totalGames = totalGames });
        return response;
    }
}