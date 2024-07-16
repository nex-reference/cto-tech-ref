namespace PipelinePoC;
public class Pipeline
{
    public Dictionary<string, List<string>> PipelineComponents { get; set; } = [];

    public HashSet<string> Components =>
        new(PipelineComponents.SelectMany(pair => pair.Value.Append(pair.Key)));
}
public class Message { }