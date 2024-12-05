namespace Web.Controllers;

/// <summary>
/// Controller responsible for administrative operations, including user, company, and business management.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet("GetAllUsers")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await adminService.GetAllUsers();
        
        return Ok(users);
    }

    /// <summary>
    /// Retrieves a list of all companies in the system.
    /// </summary>
    /// <returns>A list of companies.</returns>
    [HttpGet("GetAllCompanies")]
    [ProducesResponseType(typeof(IEnumerable<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await adminService.GetAllCompanies();
        
        return Ok(companies);
    }

    /// <summary>
    /// Retrieves the login history for a specific user.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <returns>The login history of the user.</returns>
    [HttpGet("GetUserLoginHistory")]
    [ProducesResponseType(typeof(IEnumerable<LoginHistoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserLoginHistory(Guid id)
    {
        var history = await adminService.GetUserLoginHistory(id);
        
        return Ok(history);
    }

    /// <summary>
    /// Retrieves the recent passwords of a specific user.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <returns>The user's recent passwords.</returns>
    [HttpGet("GetUserRecentPasswords")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRecentPasswords(Guid id)
    {
        var passwords = await adminService.GetUserPasswords(id);
        
        return Ok(passwords);
    }

    /// <summary>
    /// Retrieves details of a specific process by its ID.
    /// </summary>
    /// <param name="id">The process's unique identifier.</param>
    /// <returns>Details of the specified process.</returns>
    [HttpGet("GetProcessById")]
    [ProducesResponseType(typeof(ProcessDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProcessById(Guid id)
    {
        var process = adminService.GetProcessById(id);
        
        return Ok(process);
    }

    /// <summary>
    /// Deletes a specific user by their ID.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <returns>A success message.</returns>
    [HttpDelete("DeleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await adminService.DeleteUser(id);
        
        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }

    /// <summary>
    /// Deletes a specific company by its ID.
    /// </summary>
    /// <param name="id">The company's unique identifier.</param>
    /// <returns>A success message.</returns>
    [HttpDelete("DeleteCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await adminService.DeleteCompany(id);
        
        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }

    /// <summary>
    /// Deletes a specific process by its ID.
    /// </summary>
    /// <param name="id">The process's unique identifier.</param>
    /// <returns>A success message.</returns>
    [HttpDelete("DeleteProcess")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProcess(Guid id)
    {
        await adminService.DeleteProcess(id);
        
        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="dto">The DTO containing the updated information of the user.</param>
    /// <returns>
    /// The updated user details.
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDto<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync([FromBody] UserDto dto)
    {
        var bookDto = await adminService.PutUserAsync(dto);
        
        return Ok(new ResponseDto<UserDto>(CommonStrings.SuccessResultPut, data: bookDto));
    }
}