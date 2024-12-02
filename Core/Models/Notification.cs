namespace Core.Models;

public class Notification : BaseModel
{
    /// <summary>
    /// Message of notification
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// User received message
    /// </summary>
    public UserModel User { get; set; }
}