namespace PipelinePoC;
public interface IPipelineComponent
{
    ValueTask ExecuteAsync(CancellationToken stoppingToken);
}
