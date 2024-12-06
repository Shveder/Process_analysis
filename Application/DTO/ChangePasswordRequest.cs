namespace Application.DTO;

public class ChangePasswordRequest 
{
    /// <summary>
    /// ID of user
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Previous password of user
    /// </summary>
    public string PreviousPassword { get; set; }
    
    /// <summary>
    /// New password of user
    /// </summary>
    public string NewPassword { get; set; }
}