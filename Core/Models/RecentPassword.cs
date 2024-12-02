namespace Core.Models;

public class RecentPassword : BaseModel
{
    /// <summary>
    /// Recent password of user
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Model of user
    /// </summary>
    public User User { get; set; }
}