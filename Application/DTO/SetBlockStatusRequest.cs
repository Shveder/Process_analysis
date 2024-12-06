namespace Application.DTO;

public class SetBlockStatusRequest
{
    /// <summary>
    /// Id of user
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// New status of user
    /// </summary>
    public bool Status { get; set; }
}