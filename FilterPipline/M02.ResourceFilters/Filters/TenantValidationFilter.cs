using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace M02.ResourceFilters.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TenantValidationFilter(IConfiguration config) : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {

        var tenantId = context.HttpContext.Request.Headers["tenantId"].ToString();
        var apikey = context.HttpContext.Request.Headers["x-api-key"].ToString();

        var expectedKey = config[$"Tenants:{tenantId}:ApiKey"];
        if(string.IsNullOrEmpty(expectedKey) || expectedKey != apikey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        await next();
    }
}