namespace Application.DTO;

public class ProcessDto : BaseDto
{
    /// <summary>
    /// Name of the process
    /// </summary>
    public string ProcessName { get; set; }
    
    /// <summary>
    /// Status of the process
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Privicy of the process
    /// </summary>
    public bool Privicy { get; set; }
    
    /// <summary>
    /// Company of the process
    /// </summary>
    public Company Company { get; set; }
}