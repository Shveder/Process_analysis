namespace Web.Controllers;

/// <summary>
/// Controller responsible for user operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Registers a new user with the provided information.
    /// </summary>
    /// <param name="request">The registration request containing user details.</param>
    /// <returns>
    /// A success message if registration is successful.
    /// </returns>
    [HttpPut("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await userService.ChangePassword(request);

        return Ok("Password changed");
    }
}