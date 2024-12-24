namespace Core.Models;

public class Access : BaseModel
{
    /// <summary>
    /// Company of access
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Company of access
    /// </summary>
    public Company Company { get; set; }
}