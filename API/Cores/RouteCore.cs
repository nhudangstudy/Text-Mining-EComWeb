using System.Text.RegularExpressions;

namespace API.Cores
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value) => Regex.Replace((string)value!, "([a-z])([A-Z])", "$1-$2").ToLower();
    };
}
