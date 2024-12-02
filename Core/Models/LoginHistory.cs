namespace Core.Models;

public class LoginHistory : BaseModel
{
    /// <summary>
    /// Ip adress of user
    /// </summary>
    public string Ip { get; set; }
    
    /// <summary>
    /// Model of user
    /// </summary>
    public UserModel User { get; set; }
}