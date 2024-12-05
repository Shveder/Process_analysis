namespace Core.Models;

public class Comment : BaseModel
{
    /// <summary>
    /// Process commented
    /// </summary>    
    public Process Process { get; set; }

    /// <summary>
    /// User who comments
    /// </summary>    
    public User User { get; set; }
    
    /// <summary>
    /// Text of comment
    /// </summary>
    public string CommentText { get; set; }
}