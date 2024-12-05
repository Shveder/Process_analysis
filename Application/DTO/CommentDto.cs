namespace Application.DTO;

public class CommentDto : BaseDto
{
    /// <summary>
    /// Process ID commented
    /// </summary>    
    public Guid ProcessId { get; set; }

    /// <summary>
    /// Id of user who comment
    /// </summary>    
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Text of comment
    /// </summary>
    public string CommentText { get; set; }
}