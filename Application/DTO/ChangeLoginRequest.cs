namespace Application.DTO;

public class ChangeLoginRequest
{
    /// <summary>
    /// New login of user
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// ID of user
    /// </summary>
    public Guid Id { get; set; }
}