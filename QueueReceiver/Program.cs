using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Configurations;

IHostBuilder builder = ConfigureBuilder();
var host = builder.Build();

using (var cts = new CancellationTokenSource())
{
    var cancelTask = Task.Run(() =>
    {
        Console.WriteLine("Press any key to cancel...");
        Console.ReadKey(intercept: true);
        cts.Cancel();
    });

    await host.RunAsync(cts.Token);
    await cancelTask;
}

static IHostBuilder ConfigureBuilder()
{
    return new HostBuilder()
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false);

            if (hostingContext.HostingEnvironment.IsDevelopment())
            {
                config.AddJsonFile("appsettings.Development.json", optional: true);
            }

            config.AddUserSecrets<Program>();
        })
        
        .ConfigureServices((hostContext, services) =>
        {
            services.AddOptions<QueueOptions>()
            .Bind(hostContext.Configuration.GetSection(QueueOptions.SectionName))
            .ValidateOnStart();
            
            services.AddHostedService<QueueProcessor>();
        });
}