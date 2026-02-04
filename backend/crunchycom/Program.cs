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
        
        // 1. Register the Azure Table Client
        services.AddAzureClients(builder => {
            builder.AddTableServiceClient(Environment.GetEnvironmentVariable("TableStorageConnectionString"));
        });

        // 2. Register your Repository (Scoped is best for data access)
        services.AddScoped<IRepository, PostRepository>();
        
    })
    .Build();

host.Run();