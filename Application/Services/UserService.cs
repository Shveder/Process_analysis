namespace Application.Services;

[AutoInterface]
public class UserService(IDbRepository repository, IMapper mapper, IBaseService baseService) : IUserService
{
    public async Task ChangePassword(ChangePasswordRequest request)
    {
        var user = await repository.Get<User>(model => model.Id == request.Id).FirstOrDefaultAsync();

        string prevPassword = request.PreviousPassword; 
        prevPassword = Hash(prevPassword);
        prevPassword = Hash(prevPassword + user?.Salt);
        
        if (!(prevPassword == user.Password))
            throw new IncorrectDataException("Password is incorrect!");
        
        if (prevPassword == request.NewPassword)
            throw new IncorrectDataException("New password must be indifferent!");
        
        if (request.NewPassword.Length is < 4 or > 32)
            throw new IncorrectDataException("Password must be between 4 and 32 characters long!");
        
        request.NewPassword = Hash(request.NewPassword);
        request.NewPassword = Hash(request.NewPassword + user?.Salt);
        
        user.Password = request.NewPassword;
        user.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(user);
        await repository.SaveChangesAsync();

        await AddRecentPassword(user, request.PreviousPassword);
    }
    
    private async Task AddRecentPassword(User user, string oldPassword)
    {
        var recentPassword = new RecentPassword
        {
            Id = Guid.NewGuid(),
            Password = oldPassword,
            User = user
        };
        await repository.Add(recentPassword);
        await repository.SaveChangesAsync();
    }
    
    private string Hash(string inputString)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));
            
            return sb.ToString();
        }
    }
    
    public async Task<CommentDto> AddComment(CommentDto dto)
    {
        var comment = mapper.Map<Comment>(dto);
        comment.Process = baseService.GetProcessById(dto.ProcessId);
        comment.User = baseService.GetUserById(dto.UserId);

        await repository.Add(comment);
        await repository.SaveChangesAsync();
        
        return mapper.Map<CommentDto>(comment);
    }
    
    public async Task<IndicatorDto> AddIndicator(IndicatorDto dto)
    {
        var indicator = mapper.Map<Indicator>(dto);
        indicator.Process = baseService.GetProcessById(dto.ProcessId);

        await repository.Add(indicator);
        await repository.SaveChangesAsync();
        
        return mapper.Map<IndicatorDto>(indicator);
    }
    
    public async Task<RecordDto> AddRecord(RecordDto dto)
    {
        var record = mapper.Map<Record>(dto);
        record.Indicator = baseService.GetIndicatorById(dto.IndicatorId);

        await repository.Add(record);
        await repository.SaveChangesAsync();
        
        return mapper.Map<RecordDto>(record);
    }

    public async Task<IEnumerable<Notification>> GetAllNotifications(Guid id)
    {
        var entities = repository.GetAll<Notification>()
            .Where(notify => notify.User.Id == id).AsQueryable();
        
        return entities;
    }
    
    public async Task DeleteNotification(Guid id)
    {
        var entity = await repository.Get<Notification>(e => e.Id == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException($"{nameof(Notification)} {CommonStrings.NotFoundResult}");

        await repository.Delete(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task<UserDto> GetUserById(Guid id)
    {
        var entity = await repository.Get<User>(e => e.Id == id)
            .FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        var dto = mapper.Map<UserDto>(entity);
       
        return dto;
    }
}