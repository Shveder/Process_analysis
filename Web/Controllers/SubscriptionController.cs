namespace Web.Controllers;

/// <summary>
/// Controller responsible for add, delete and check subscription.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubscriptionController(ISubscriptionService subscriptionService) : ControllerBase
{
    /// <summary>
    /// Registers a new user with the provided information.
    /// </summary>
    /// <param name = "request">The add subscription request containing sub details.</param>
    /// <returns>
    /// A success message if registration is successful.
    /// </returns>
    [HttpPost("AddSubscription")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSubscription([FromBody] SubscriptionRequest request)
    {
        await subscriptionService.AddSubscription(request);

        return Ok("Подписка добавлена");
    }
    /// <summary>
    ///     Delete subscription
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Delete /Subscription/DeleteSubscription
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Subscription is deleted.</response>
    /// <response code="422">Invalid input data.</response>
    [HttpDelete("DeleteSubscription")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteSubscription([FromBody] SubscriptionRequest request)
    {
        await subscriptionService.DeleteSubscription(request);

        return Ok("Подписка удалена");
    }
    
    /// <summary>
    ///     Get is subscribed
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Delete /Subscription/GetIsSubscribed
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Got is suscribed.</response>
    [HttpGet("GetIsSubscribed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIsSubscribed(Guid userId, Guid businessId)
    {
        return Ok(subscriptionService.GetIsSubscribed(new SubscriptionRequest(userId, businessId)));
    }
}