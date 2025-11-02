using M02.QueryString.Models;
using Microsoft.AspNetCore.Mvc;

namespace M02.QueryString.controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller")]
    public IActionResult Get(int page, int pageSize)
    {
        return Ok($"Showing {pageSize} items of page # {page}");
    }

    [HttpGet("product-controller-complex-query")]
    public IActionResult GetComplexQuery([FromQuery] SearchRequest request)
    {
        return Ok(request);
    }

    [HttpGet("product-controller-array")]
    public IActionResult GetComplexQuery([FromQuery] Guid[] ids)
    {
        return Ok(ids);
    }

    [HttpGet("date-range-controller")]
    public IActionResult GetComplexQuery(DateRangeQuery dateRange)
    {
        return Ok(dateRange);
    }

}