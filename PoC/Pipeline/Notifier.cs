using Microsoft.Extensions.Logging;

namespace PipelinePoC;
public class Notifier(ILogger<Notifier> logger, ChannelFactory channelFactory) : IPipelineComponent
{
    public async ValueTask ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Notifier is starting");

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
