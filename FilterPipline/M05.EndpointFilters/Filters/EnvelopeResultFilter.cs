
namespace M05.EndpointFilters.Filters;

public class EnvelopeResultFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        return Results.Json(new
        {
            success = true,
            data = result
        });
    }
}
