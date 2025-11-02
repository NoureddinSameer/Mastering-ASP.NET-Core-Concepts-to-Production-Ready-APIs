using Microsoft.AspNetCore.Mvc;

namespace M01.RouteParameter.controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller/{id:int}")]
    public IActionResult Get(int id)
    {
        return Ok(id);
    }

    [HttpGet("product-controller-1/{id:int}")]
    public IActionResult Get1([FromQuery]int id)
    {
        return Ok(id);
    }

}