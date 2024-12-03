using Common;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    /// <summary>
    /// Retrieves a book by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the company.</param>
    /// <returns>
    /// The book details corresponding to the provided ID.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var bookDto = await companyService.GetByIdAsync(id);
        
        return Ok(new ResponseDto<CompanyDto>(CommonStrings.SuccessResult, data: bookDto));
    }
    
    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="dto">The DTO containing the information of the company to create.</param>
    /// <returns>
    /// The created book details.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] CompanyDto dto)
    {
        var bookDto = await companyService.PostAsync(dto);
        
        return Ok(new ResponseDto<CompanyDto>(CommonStrings.SuccessResultPost, data: bookDto));
    }

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <param name="dto">The DTO containing the updated information of the company.</param>
    /// <returns>
    /// The updated book details.
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDto<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync([FromBody] CompanyDto dto)
    {
        var bookDto = await companyService.PutAsync(dto);
        
        return Ok(new ResponseDto<CompanyDto>(CommonStrings.SuccessResultPut, data: bookDto));
    }

    /// <summary>
    /// Deletes a book by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the company to delete.</param>
    /// <returns>
    /// A success message if the book is deleted.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await companyService.DeleteByIdAsync(id);
        
        return Ok(new ResponseDto<string>(CommonStrings.SuccessResultDelete));
    }
    
    /// <summary>
    /// Retrieves all companies.
    /// </summary>
    /// <returns>An IActionResult containing the list of all companies.</returns>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var entity = await companyService.GetAllAsync();
        
        return Ok(new ResponseDto<IEnumerable<CompanyDto>>(CommonStrings.SuccessResult, data: entity));
    }
}