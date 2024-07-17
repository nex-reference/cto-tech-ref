using Microsoft.Extensions.Logging;
using PipelinePoC.Messaging;

namespace PipelinePoC.Components;
public class AnomalyLogger(ILogger<AnomalyLogger> logger, ChannelFactory channelFactory) : IPipelineComponent
{
    public async ValueTask ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("AnomalyLogger is starting");

        var currentComponent = typeof(Renderer).FullName!;

        await foreach (var message in channelFactory.Reader(currentComponent).ReadAllAsync(stoppingToken))
        {
            // process message
            foreach (var writer in channelFactory.Writers(currentComponent))
            {
                await writer.WriteAsync(message, stoppingToken);
            }
        }
    }
}
