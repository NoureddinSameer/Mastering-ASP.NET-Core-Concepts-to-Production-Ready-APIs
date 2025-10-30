using Microsoft.AspNetCore.Mvc;

namespace M01.RoutingBasics.Controllers;


[ApiController]
[Route("[controller]")]// ../Products

public class ProductsController: ControllerBase
{
    // ../products/all
    [HttpGet("all")]
    public IActionResult GetProducts()
    {
        return Ok(new[]
        {
            "Product #1",
            "Product #2"
        });
    }
}