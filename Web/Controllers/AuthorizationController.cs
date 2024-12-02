namespace Web.Controllers;

/// <summary>
/// Controller responsible for user authorization, including login and registration functionalities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    [HttpGet]
    [Route("api/authorization")]
    public IActionResult Get()
    {
        return Ok("dasdasdada");
    }
}