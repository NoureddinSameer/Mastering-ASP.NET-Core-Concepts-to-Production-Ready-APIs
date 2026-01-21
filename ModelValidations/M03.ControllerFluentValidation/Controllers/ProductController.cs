using M03.ControllerFluentValidation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace M03.ControllerFluentValidation.Controllers;

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