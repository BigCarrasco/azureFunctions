using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Company.Function.Contracts;
using Company.Function.Infrastructure.Storage;
using Company.Function.Application.Services; 

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<XboxGameService>();
builder.Services.AddSingleton<ILoggingService, FileLoggingService>();


builder.Build().Run();
