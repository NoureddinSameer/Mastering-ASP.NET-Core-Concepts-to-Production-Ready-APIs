using Microsoft.AspNetCore.Mvc;
using M02.ResourceFilters.Filters;

namespace M02.ResourceFilters.Controllers;

[ApiController]
[Route("api/products")]

public class ProductController() : ControllerBase
{

    [HttpGet]
    [ServiceFilter(typeof(TenantValidationFilter))]
    public IActionResult Get()
    {
        return Ok(new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" });
    }

    [HttpGet]
    [Route("get2")]
    public IActionResult Get2()
    {
        return Ok(new[] { "Keyboard2 [$52.99]", "Mouse2, [$34.99]" });
    }
}

