using M03.SecureRESTAPIWithJWTAuthentication.Requests;
using M03.SecureRESTAPIWithJWTAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace M03.SecureRESTAPIWithJWTAuthentication.Controllers;

[ApiController]
[Route("api/Token")]
public class TokenController(JwtTokenProvider tokenProvider): ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GenerateToken(GenerateTokenRequest request)
    {
        return Ok(tokenProvider.GenerateJwtToken(request));
    }
}