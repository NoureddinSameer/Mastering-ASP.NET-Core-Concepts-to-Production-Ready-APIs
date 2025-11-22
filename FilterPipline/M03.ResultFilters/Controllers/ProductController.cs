using M03.ResultFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace M03.ResultFilters.Controllers;

[ApiController]
[Route("api/products")]
[EnvelopeResultFilter]
public class ProductController() : ControllerBase
{

    [HttpGet]
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

