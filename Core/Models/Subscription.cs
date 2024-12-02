namespace Core.Models;

public class Subscription : BaseModel
{
    /// <summary>
    /// User of subscription
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Process of subscription
    /// </summary>
    public Process Process { get; set; }
}