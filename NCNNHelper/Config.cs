using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace NCNNHelper;

public class Config
{
    public string? ProcessName { get; set; }
    public Arg[]? Args { get; set; }
}

public class Arg
{
    public string? Option { get; set; }
    public string? Description { get; set; }
    public string? Default { get; set; }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Config))]
public partial class ConfigContext : JsonSerializerContext
{
}