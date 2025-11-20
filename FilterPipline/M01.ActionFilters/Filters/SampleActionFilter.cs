using Microsoft.AspNetCore.Mvc.Filters;

namespace M01.ActionFilters.Filters;

public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Sample Action Filter Sync Before");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Sample Action Filter Sync After");

    }


}
// Improve the above class to Async
public class SampleActionFilterAsync : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Sample Action Filter Async Before");

        await next();

        Console.WriteLine("Sample Action Filter Async After");

    }
}