using Microsoft.AspNetCore.Mvc;

namespace M01.FiltersController.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController() : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" });
    }
}

