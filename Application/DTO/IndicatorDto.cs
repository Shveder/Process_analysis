namespace Application.DTO;

public class IndicatorDto : BaseDto
{
    /// <summary>
    /// Name of the indicator
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Process ID of indicator
    /// </summary>
    public Guid ProcessId { get; set; }
    
    /// <summary>
    /// Measurement of indicator
    /// </summary>
    public string Measurement { get; set; }
    
    /// <summary>
    /// Significance of indicator
    /// </summary>
    public string Significance { get; set; }
}