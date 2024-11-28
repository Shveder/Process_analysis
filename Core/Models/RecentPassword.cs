namespace Core.Models;

public class RecentPassword
{
    /// <summary>
    /// Recent password of user
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Model of user
    /// </summary>
    public UserModel User { get; set; }
}