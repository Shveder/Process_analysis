namespace Core.Models;

public class Record : BaseModel
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
    /// Indicator of record
    /// </summary>
    public Indicator Indicator { get; set; }
}