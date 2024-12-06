namespace Application.DTO;

public class ChangeRoleRequest
{
    /// <summary>
    /// ID of user
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// New role of user
    /// </summary>
    public string Role { get; set; }
}