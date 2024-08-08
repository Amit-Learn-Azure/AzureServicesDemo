using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
     .ConfigureAppConfiguration((host, configBuilder) =>
     {
       configBuilder.AddUserSecrets<Program>();

            configBuilder.AddAzureKeyVault(
                 new Uri("https://amit-keyvaultqueue.vault.azure.net/"),
                 new DefaultAzureCredential(),
                 new AzureKeyVaultConfigurationOptions
                 {
                     ReloadInterval = TimeSpan.FromMinutes(15)
                 });
         
     })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
