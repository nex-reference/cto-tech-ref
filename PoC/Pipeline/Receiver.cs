using Microsoft.Extensions.Logging;

namespace PipelinePoC;
public class Receiver(ILogger<Receiver> logger, ChannelFactory channelFactory) : IPipelineComponent
{
    public async ValueTask ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Receiver is starting.");

        var currentComponent = typeof(Receiver).FullName!;

        foreach (var channel in channelFactory.Writers(currentComponent))
        {
            await channel.WriteAsync(new Message(), stoppingToken);
        }
    }
}
