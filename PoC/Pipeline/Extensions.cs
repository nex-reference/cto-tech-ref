using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PipelinePoC;
public static class Extensions
{
    public static IServiceCollection AddPipelineComponents(this IServiceCollection services, IConfiguration config)
    {
        var pipelineConfig = config.GetSection("PipelineComponents").Get<Dictionary<string, List<string>>>();

        ArgumentNullException.ThrowIfNullOrEmpty(nameof(pipelineConfig));

        var pipelineComponents = new Pipeline
        {
            PipelineComponents = pipelineConfig!
        };

        services.AddSingleton(pipelineComponents);

        foreach (var component in pipelineComponents.Components)
        {
            var type = Type.GetType(component);

            ArgumentNullException.ThrowIfNull(type, $"Type {component} not found");

            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IPipelineComponent), type));
        }

        return services;
    }
}


