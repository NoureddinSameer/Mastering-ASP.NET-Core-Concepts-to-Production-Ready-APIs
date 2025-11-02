
using M05.Body.Requests;
using Microsoft.AspNetCore.Mvc;

namespace M05.QueryString.controllers;

[ApiController]
public class ProductController : ControllerBase
{

    [HttpPost("product-controller")]
    public IActionResult Post(ProductRequest request)
    {
        return Ok(request);
    }
}