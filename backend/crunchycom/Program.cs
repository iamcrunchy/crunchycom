using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration; // Needed for configuration access

using CrunchyCom.Data;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// 1. Grab the connection string from local.settings.json or Azure Portal Settings
// builder.Configuration handles the Environment Variables automatically
var connectionString = builder.Configuration["TableStorageConnectionString"];

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("TableStorageConnectionString is not set.");
}

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    // 2. Register your Repository
    .AddSingleton(new PostRepository(connectionString)); 

builder.Build().Run();