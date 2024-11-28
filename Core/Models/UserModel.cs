namespace Core.Models;

public class UserModel : BaseModel
{
    /// <summary>
    /// Login of user
    /// </summary>
    public string Login { get; set; } 
    
    /// <summary>
    /// Password of user
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Salt of user password
    /// </summary>
    public string Salt { get; set; } 
    
    /// <summary>
    /// Role of user 
    /// </summary>
    public string Role { get; set; }
    
    /// <summary>
    /// Block status of user
    /// </summary>
    public bool IsBlocked { get; set; }
    
    /// <summary>
    /// Delete status of user
    /// </summary>
    public bool IsDeleted { get; set; }
}