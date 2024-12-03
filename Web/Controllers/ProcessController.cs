namespace Web.Controllers;

/// <summary>
/// Controller responsible for process crud operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProcessController(IProcessService processService) : ControllerBase
{
    /// <summary>
    /// Retrieves a process by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the process.</param>
    /// <returns>
    /// The process details corresponding to the provided ID.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<ProcessDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var bookDto = await processService.GetByIdAsync(id);
        
        return Ok(new ResponseDto<ProcessDto>(CommonStrings.SuccessResult, data: bookDto));
    }
    
    /// <summary>
    /// Creates a new process.
    /// </summary>
    /// <param name="dto">The DTO containing the information of the process to create.</param>
    /// <returns>
    /// The created process details.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<ProcessDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] ProcessDto dto)
    {
        var bookDto = await processService.PostAsync(dto);
        
        return Ok(new ResponseDto<ProcessDto>(CommonStrings.SuccessResultPost, data: bookDto));
    }

    /// <summary>
    /// Updates an existing process.
    /// </summary>
    /// <param name="dto">The DTO containing the updated information of the process.</param>
    /// <returns>
    /// The updated process details.
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDto<ProcessDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync([FromBody] ProcessDto dto)
    {
        var bookDto = await processService.PutAsync(dto);
        
        return Ok(new ResponseDto<ProcessDto>(CommonStrings.SuccessResultPut, data: bookDto));
    }

    /// <summary>
    /// Deletes a process by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the process to delete.</param>
    /// <returns>
    /// A success message if the process is deleted.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await processService.DeleteByIdAsync(id);
        
        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }
    
    /// <summary>
    /// Retrieves all processes.
    /// </summary>
    /// <returns>An IActionResult containing the list of all processes.</returns>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var entity = await processService.GetAllAsync();
        
        return Ok(new ResponseDto<IEnumerable<ProcessDto>>(CommonStrings.SuccessResult, data: entity));
    }
}