namespace Core.Models;

public class Indicator : BaseModel
{
    /// <summary>
    /// Name of the indicator
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Process of indicator
    /// </summary>
    public Process Process { get; set; }
    
    /// <summary>
    /// Measurement of indicator
    /// </summary>
    public string Measurement { get; set; }
    
    /// <summary>
    /// Significance of indicator
    /// </summary>
    public string Significance { get; set; }
}