using System.Threading.Channels;

namespace PipelinePoC;
public class ChannelFactory
{
    private readonly Dictionary<string, List<ChannelWriter<Message>>> writers = [];
    private readonly Dictionary<string, ChannelReader<Message>> readers = [];
    public ChannelFactory(Pipeline pipeline)
    {
        foreach (var kvp in pipeline.PipelineComponents)
        {
            writers.TryAdd(kvp.Key, []);

            foreach (var item in kvp.Value)
            {
                var writer = ChannelManager.Create<Message>();
                writers[kvp.Key].Add(writer);
                readers.TryAdd(item, writer.Reader);
            }
        }
    }
    public List<ChannelWriter<Message>> Writers(string key)
    {
        if (writers.TryGetValue(key, out var writerList))
        {
            return writerList;
        }

        throw new KeyNotFoundException($"No writers found for key {key}");
    }

    public ChannelReader<Message> Reader(string key)
    {
        if (readers.TryGetValue(key, out var reader))
        {
            return reader;
        }

        throw new KeyNotFoundException($"No reader found for key {key}");
    }
}
public class ChannelManager
{
    public static Channel<T> Create<T>() => Channel.CreateUnbounded<T>();
}
