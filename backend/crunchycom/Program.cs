using crunchycom.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration; // Needed for configuration access

using CrunchyCom.Data;
using Microsoft.Extensions.Azure;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        var connectionString = Environment.GetEnvironmentVariable("TableStorageConnectionString");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception(
                "missing environment variable 'TableStorageConnectionString'. " +
                "Set it in local.settings.json, or in the run configuration environment variables.");
        }
        // 1. Register the Azure Table Client
        services.AddAzureClients(builder => {
            builder.AddTableServiceClient(connectionString);
        });

        // 2. Register your Repository (Scoped is best for data access)
        services.AddScoped<IRepository, PostRepository>();
        
    })
    .Build();

host.Run();