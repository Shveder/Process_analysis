namespace Application.Services;

[AutoInterface]
public class SubscriptionService(IDbRepository repository, IBaseService baseService) : ISubscriptionService
{
     public async Task AddSubscription(SubscriptionRequest request)
    {
        if (GetIsSubscribed(request))
            throw new IncorrectDataException("You already subscribed.");
        
        var user = baseService.GetUserById(request.UserId);
        var process = GetProcessById(request.ProcessId);

        var subscription = new Subscription()
        {
            User = user,
            Process = process
        };
        await repository.Add(subscription);
        await repository.SaveChangesAsync();
    }

    public async Task DeleteSubscription(SubscriptionRequest request)
    {
        if (!GetIsSubscribed(request))
            throw new IncorrectDataException("You are not subscribed.");
        
        var user = baseService.GetUserById(request.UserId);
        var process = GetProcessById(request.ProcessId);

        var subscription = repository.Get<Subscription>(subscription =>
            subscription.Process == process
            && subscription.User == user).FirstOrDefault();
        
        if (subscription is null)
            throw new EntityNotFoundException($"{nameof(Subscription)} {CommonStrings.NotFoundResult}");

        await repository.Delete(subscription);
        await repository.SaveChangesAsync();
    }

    public async Task Notify(Guid userId, string text)
    {
        var user = baseService.GetUserById(userId);
        var notification = new Notification()
        {
            User = user,
            Message = text
        };
        await repository.Add(notification);
        await repository.SaveChangesAsync();
    }
    public bool GetIsSubscribed(SubscriptionRequest request)
    {
        var user = baseService.GetUserById(request.UserId);
        var process = GetProcessById(request.ProcessId);
    
        var subscription = repository.Get<Subscription>(subscription =>
            subscription.Process == process
            && subscription.User == user).FirstOrDefault();
        
        return subscription != null;
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscribers(Guid processId)
    {
        var process = GetProcessById(processId);
        var subscriptions = await repository.Get<Subscription>(s =>
            s.Process == process).Include(s => s.User).ToListAsync();
        
        return subscriptions;
    }
    
    private Process GetProcessById(Guid id)
    {
        var process = repository.Get<Process>(model => model.Id == id).FirstOrDefault();
        if (process == null)
            throw new IncorrectDataException("There is no process with this id");
        
        return process;
    }
}