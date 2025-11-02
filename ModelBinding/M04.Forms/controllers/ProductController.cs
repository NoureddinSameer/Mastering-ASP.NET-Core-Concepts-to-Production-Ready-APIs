
using Microsoft.AspNetCore.Mvc;

namespace M02.QueryString.controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller")]
    public IActionResult Get([FromForm] string name, [FromForm] decimal price)
    {
        return Ok(new { name, price });
    }


    [HttpPost("upload-controller")]
    public async Task<IActionResult> Post(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("no file uploaded");

        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        Directory.CreateDirectory(uploads);

        var path = Path.Combine(uploads, file.FileName);
        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok("uploaded");
    }
}