
using Microsoft.AspNetCore.Mvc;

namespace M02.QueryString.controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller")]
    public IActionResult Get([FromHeader(Name="X-Api-version")]string apiVersion)
    {
        return Ok($"Api version: {apiVersion}");
    }
}