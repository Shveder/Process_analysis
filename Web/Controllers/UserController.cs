using Core.Models;

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
    
    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="dto">The DTO containing the information of the comment to create.</param>
    /// <returns>
    /// The created process details.
    /// </returns>
    [HttpPost]
    [Route("AddComment")]
    [ProducesResponseType(typeof(ResponseDto<CommentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment([FromBody] CommentDto dto)
    {
        var commentDto = await userService.AddComment(dto);
        
        return Ok(new ResponseDto<CommentDto>(CommonStrings.SuccessResultPost, data: commentDto));
    }
    
    /// <summary>
    /// Creates a new indicator to process.
    /// </summary>
    /// <param name="dto">The DTO containing the information of the indicator to create.</param>
    /// <returns>
    /// The created Indicator detail.
    /// </returns>
    [HttpPost]
    [Route("AddIndicator")]
    [ProducesResponseType(typeof(ResponseDto<IndicatorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddIndicator([FromBody] IndicatorDto dto)
    {
        var indicatorDto = await userService.AddIndicator(dto);
        
        return Ok(new ResponseDto<IndicatorDto>(CommonStrings.SuccessResultPost, data: indicatorDto));
    }
    
    /// <summary>
    /// Creates a new record to indicator.
    /// </summary>
    /// <param name="dto">The DTO containing the information of the record to create.</param>
    /// <returns>
    /// The created record details.
    /// </returns>
    [HttpPost]
    [Route("AddRecord")]
    [ProducesResponseType(typeof(ResponseDto<RecordDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRecord([FromBody] RecordDto dto)
    {
        var recordDto = await userService.AddRecord(dto);
        
        return Ok(new ResponseDto<RecordDto>(CommonStrings.SuccessResultPost, data: recordDto));
    }
}