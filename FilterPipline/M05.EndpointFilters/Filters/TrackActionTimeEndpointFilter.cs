
namespace M05.EndpointFilters.Filters;

public class TrackActionTimeEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var start = DateTime.UtcNow;

        var result = await next(context);

        var elapsed = DateTime.UtcNow - start;

        context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"{elapsed.TotalMilliseconds}ms");

        Console.WriteLine($"Track Time Filter Took {elapsed.TotalMilliseconds}ms");

        return result;
    }
}