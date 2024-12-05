namespace Application.DTO;

public class ChangePasswordRequest : BaseDto
{
    /// <summary>
    /// Previous password of user
    /// </summary>
    public string PreviousPassword { get; set; }
    
    /// <summary>
    /// New password of user
    /// </summary>
    public string NewPassword { get; set; }
}