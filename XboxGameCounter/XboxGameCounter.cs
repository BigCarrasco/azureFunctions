using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class XboxGameCounter
    {
        private readonly ILogger<XboxGameCounter> _logger;

        public XboxGameCounter(ILogger<XboxGameCounter> logger)
        {
            _logger = logger;
        }

        [Function("XboxGameCounter")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var httpClient = new HttpClient();
            var apiresponse = httpClient.GetAsync("https://api.sampleapis.com/xbox/games");
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");


            if (!apiResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al llamar a la API externa: {apiResponse.StatusCode}");
                var errorResponse = req.CreateResponse(HttpStatusCode.BadGateway);
                await errorResponse.WriteStringAsync("No se pudo obtener la lista de juegos.");
                return errorResponse;
            }

            var content = await apiResponse.Content.ReadAsStringAsync();


        }
    }
}