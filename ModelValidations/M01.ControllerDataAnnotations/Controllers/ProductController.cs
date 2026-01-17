using M01.BasicSetup.Requests;
using Microsoft.AspNetCore.Mvc;

namespace M01.ControllerDataAnnotations.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{

    [HttpPost]
    public IActionResult Post(CreateProductRequest request)
    {
        // No need if [ApiController] is present
        // if(!ModelState.IsValid)
        //     return ValidationProblem(ModelState);
        return Created($"/api/products/{Guid.NewGuid()}", request);
    }
}