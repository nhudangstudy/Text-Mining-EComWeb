using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

// Ensure the class implements the correct interface
public class SetHostAndSchemesFilter : IDocumentFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetHostAndSchemesFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var host = request.Host.Value;

        if (swaggerDoc.Servers.Count == 0)
        {
            swaggerDoc.Servers.Add(new OpenApiServer
            {
                Url = $"{request.Scheme}://{host}"
            });
        }
    }
}
