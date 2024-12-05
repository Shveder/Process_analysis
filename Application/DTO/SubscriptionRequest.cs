namespace Application.DTO;

public class SubscriptionRequest(Guid userId, Guid processId)
{ 
    /// <summary>
    /// ID of User
    /// </summary>
    public Guid UserId { get; set; } = userId;

    /// <summary>
    /// ID of Process
    /// </summary>
    public Guid ProcessId { get; set; } = processId;
}