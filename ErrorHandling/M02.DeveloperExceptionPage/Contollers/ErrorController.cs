using Microsoft.AspNetCore.Mvc;

namespace M02.DeveloperExceptionPage.Contollers;
public class ErrorController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error()=>
    new ObjectResult(new
    {
       StatusCode =500,
       Message = "Internal Server Error!"
    });
}