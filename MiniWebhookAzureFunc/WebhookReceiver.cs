using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.IO; // Necesario para StreamReader

namespace MiniWebhookAzureFunc;

public class WebhookReceiver
{
    private readonly ILogger _logger;

    public WebhookReceiver(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<WebhookReceiver>();
    }


    [Function("WebhookReceiver")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request (Webhook received).");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        if (string.IsNullOrEmpty(requestBody))
        {
            _logger.LogWarning("Received an empty webhook payload body. Returning 400 Bad Request.");

            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            badRequestResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await badRequestResponse.WriteStringAsync("Error: Request body is empty. A webhook payload is expected.");

            return badRequestResponse;
        }


        _logger.LogInformation($"Webhook Payload Received:");
        _logger.LogInformation(requestBody);
        _logger.LogInformation("--------------------------------------------------");


        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync("Webhook received and body was not empty.");

        return response; 
    }
}

/*
{
    "id": "evt_124567890abc",
    "type": "payment_succeeded",
    "created": 1678886400,
    "data": {
        "object": {
            "id": "pay_abcdef123456",
            "amount": 10000,
            "currency": "mxn",
            "status": "succeeded",
            "customer": "cus_xyz987",
            "metadata": {
                "order_id": "order_abcde"
            },
            "amount_captured": 10000,
            "amount_received": 10000
        }
    },
    "livemode": false,
    "api_version": "2024-03-14",
    "request": {
        "id": "req_abcdef123456",
        "idemkey": "..."
    },
    "pending_webhooks": 1
}

*/