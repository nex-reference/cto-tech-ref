using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PipelinePoC;
public class PipelineService(ILogger<PipelineService> logger, IEnumerable<IPipelineComponent> pipelineComponents) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("PipelineService is starting.");
        try
        {
            var tasks = pipelineComponents.Select(module => module.ExecuteAsync(stoppingToken).AsTask());
            await Task.WhenAll(tasks);
        }
        catch (AggregateException ae)
        {
            foreach (var ex in ae.Flatten().InnerExceptions)
            {
                logger.LogError(ex, "Error executing modules");
            }
        }
    }
}
