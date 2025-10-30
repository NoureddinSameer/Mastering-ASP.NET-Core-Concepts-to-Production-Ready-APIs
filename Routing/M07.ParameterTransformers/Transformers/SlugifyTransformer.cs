using System.Text.RegularExpressions;

namespace  M07.ParameterTransformers.Transformers;

public class SlugifyTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value is null
        ? null
        : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2")
        .Replace(" ", "-")
        .ToLowerInvariant();
    }
}