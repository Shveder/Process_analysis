namespace Application.DTO;

public class RecordDto : BaseDto
{
    /// <summary>
    /// Value of record
    /// </summary>
    public double Value { get; set; }
    
    /// <summary>
    /// Time of record
    /// </summary>
    public DateTime Time { get; set; }
    
    /// <summary>
    /// Indicator ID of record
    /// </summary>
    public Guid IndicatorId { get; set; }
}