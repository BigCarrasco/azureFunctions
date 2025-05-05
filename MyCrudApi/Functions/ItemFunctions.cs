using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using MyCrudApi.Models;
using MyCrudApi.Repositories;
using System.Text.Json;

namespace MyCrudApi.Functions;

public class ItemFunctions
{
    private readonly ILogger _logger;
    private readonly InMemoryItemRepository _repo;

    public ItemFunctions(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ItemFunctions>();
        _repo = new InMemoryItemRepository();
    }

    [Function("GetItems")]
    public HttpResponseData GetItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(_repo.GetAll());
        return response;
    }

    [Function("GetItemById")]
    public async Task<HttpResponseData> GetItemById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items/{id}")] HttpRequestData req, string id)
    {
        var item = _repo.Get(id);
        var response = req.CreateResponse(item is null ? HttpStatusCode.NotFound : HttpStatusCode.OK);
        await response.WriteAsJsonAsync(item ?? (object)new { message = "Item not found" });
        return response;
    }

    [Function("CreateItem")]
    public async Task<HttpResponseData> CreateItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items")] HttpRequestData req)
    {
        var body = await JsonSerializer.DeserializeAsync<Item>(req.Body);
        if (body is null)
        {
            var badRes = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRes.WriteStringAsync("Invalid payload");
            return badRes;
        }

        _repo.Add(body);
        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(body);
        return response;
    }

    [Function("UpdateItem")]
    public async Task<HttpResponseData> UpdateItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")] HttpRequestData req, string id)
    {
        var body = await JsonSerializer.DeserializeAsync<Item>(req.Body);
        if (body is null)
        {
            var badRes = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRes.WriteStringAsync("Invalid payload");
            return badRes;
        }

        var updated = _repo.Update(id, body);
        var response = req.CreateResponse(updated ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        if (updated)
        {
            await response.WriteAsJsonAsync(body);
        }
        else
        {
            await response.WriteAsJsonAsync(new { message = "Item not found" });
        }
        return response;
    }

    [Function("DeleteItem")]
    public HttpResponseData DeleteItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")] HttpRequestData req, string id)
    {
        var deleted = _repo.Delete(id);
        var response = req.CreateResponse(deleted ? HttpStatusCode.NoContent : HttpStatusCode.NotFound);
        return response;
    }
}
