﻿using Core.Models;

namespace Web.Controllers;

/// <summary>
/// Controller responsible for user operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Changes a password of user.
    /// </summary>
    /// <param name="request">The request containing user details.</param>
    /// <returns>
    /// A success message if changing is successful.
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

    /// <summary>
    /// Retrieves all notifications.
    /// </summary>
    /// <returns>An IActionResult containing the list of all notifications.</returns>
    [HttpGet("GetAllNotifications")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAllNotifications(Guid userId)
    {
        var entity = await userService.GetAllNotifications(userId);

        return Ok(new ResponseDto<IEnumerable<Notification>>(CommonStrings.SuccessResult, data: entity));
    }

    /// <summary>
    /// Deletes a notification by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the notification to delete.</param>
    /// <returns>
    /// A success message if the notification is deleted.
    /// </returns>
    [HttpDelete]
    [Route("DeleteNotification")]
    [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        await userService.DeleteNotification(id);

        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }

    /// <summary>
    /// Retrieves a user by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>
    /// The user details corresponding to the provided ID.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<ProcessDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var userDto = await userService.GetUserById(id);

        return Ok(new ResponseDto<UserDto>(CommonStrings.SuccessResult, data: userDto));
    }

    /// <summary>
    /// Changes a login of user.
    /// </summary>
    /// <param name="request">The request containing user details.</param>
    /// <returns>
    /// A success message if changing is successful.
    /// </returns>
    [HttpPut("ChangeLogin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeLogin([FromBody] ChangeLoginRequest request)
    {
        await userService.ChangeLogin(request);

        return Ok("Login changed");
    }

    /// <summary>
    /// Retrieves all processes by user ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all user processes.</returns>
    [HttpGet("GetUserProcesses")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetUserProcesses(Guid userId)
    {
        var entity = await userService.GetUserProcesses(userId);

        return Ok(new ResponseDto<IEnumerable<Process>>(CommonStrings.SuccessResult, data: entity));
    }

    /// <summary>
    /// Retrieves all indicators by process ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all process indicators.</returns>
    [HttpGet("GetIndicatorsOfProcess")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetIndicatorsOfProcess(Guid processId)
    {
        var entity = await userService.GetIndicatorsOfProcess(processId);

        return Ok(new ResponseDto<IEnumerable<IndicatorDto>>(CommonStrings.SuccessResult, data: entity));
    }

    /// <summary>
    /// Deletes a indicator by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the indicator to delete.</param>
    /// <returns>
    /// A success message if the indicator is deleted.
    /// </returns>
    [HttpDelete("DeleteIndicator/{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteIndicator(Guid id)
    {
        await userService.DeleteIndicatorByIdAsync(id);

        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }

    /// <summary>
    /// Retrieves all comments by process ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all process comments.</returns>
    [HttpGet("GetCommentsByProcessId")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetCommentsByProcessId(Guid processId)
    {
        var entity = await userService.GetCommentsByProcessId(processId);

        return Ok(new ResponseDto<IEnumerable<Comment>>(CommonStrings.SuccessResult, data: entity));
    }
    
    /// <summary>
    /// Retrieves all records by indicator ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all process comments.</returns>
    [HttpGet("GetRecordsByIndicatorId")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetRecordsByIndicatorId(Guid indicatorId)
    {
        var entity = await userService.GetRecordsByIndicatorId(indicatorId);

        return Ok(new ResponseDto<IEnumerable<Record>>(CommonStrings.SuccessResult, data: entity));
    }
    
    /// <summary>
    /// Retrieves count of notifications by user ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all user.</returns>
    [HttpGet("GetCountOfNotifications")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetCountOfNotifications(Guid userId)
    {
        return Ok(userService.GetCountOfNotifications(userId));
    }
    
    /// <summary>
    /// Retrieves notifications by user ID.
    /// </summary>
    /// <returns>An IActionResult containing the list of all notifications.</returns>
    [HttpGet("GetNotifications")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetNotifications(Guid userId)
    {
        var entity = await userService.GetNotifications(userId);
        
        return Ok(new ResponseDto<IEnumerable<Notification>>(CommonStrings.SuccessResult, data: entity));
    }
}