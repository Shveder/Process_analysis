namespace Application.DTO;

public class LoginHistoryDto : BaseDto
{
    /// <summary>
    /// Ip adress of user
    /// </summary>
    public string Ip { get; set; }
    
    /// <summary>
    /// Model of user
    /// </summary>
    public User User { get; set; }
}