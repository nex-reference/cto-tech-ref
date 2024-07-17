using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PipelinePoC;
using PipelinePoC.Messaging;

await CreateHostBuilder(args)
    .Build()
    .RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<PipelineService>();
            services.AddPipelineComponents(hostContext.Configuration);
            services.AddSingleton<ChannelFactory>();
        });


