namespace Application.DTO;

public class CompanyDto : BaseDto
{
    /// <summary>
    /// Name of the company
    /// </summary>    
    public string Name { get; set; }
    
    /// <summary>
    /// Phone of the company
    /// </summary>    
    public string Phone { get; set; }
    
    /// <summary>
    /// Email of the company
    /// </summary>    
    public string Email { get; set; }
}