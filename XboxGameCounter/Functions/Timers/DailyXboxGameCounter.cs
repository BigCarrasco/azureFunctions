using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Timer;
using Microsoft.Extensions.Logging;
using Company.Function.Application.Services;

namespace Company.Function.Functions.Timers;

public class DailyXboxGameCounter
{
    private readonly ILogger<DailyXboxGameCounter> _logger;
    private readonly XboxGameService _xboxGameService;

    public DailyXboxGameCounter(ILogger<DailyXboxGameCounter> logger, XboxGameService xboxGameService)
    {
        _logger = logger;
        _xboxGameService = xboxGameService;
    }

    [Function("DailyXboxGameCounter")]
    public async Task Run([TimerTrigger("0 0 18 * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation($"Timer trigger ejecutado a las: {DateTime.Now}");

        var totalGames = await _xboxGameService.GetTotalGamesAsync();

        _logger.LogInformation($"Total de juegos registrados hoy: {totalGames}");
    }
}