namespace Core.Models;

public class Notification
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