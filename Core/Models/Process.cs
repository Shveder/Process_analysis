namespace Core.Models;

public class Process : BaseModel
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