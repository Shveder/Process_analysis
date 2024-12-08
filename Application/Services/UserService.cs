namespace Application.Services;

[AutoInterface]
public class UserService(IDbRepository repository, IMapper mapper, IBaseService baseService) : IUserService
{
    public async Task ChangePassword(ChangePasswordRequest request)
    {
        var user = baseService.GetUserById(request.Id);

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
    
    public async Task ChangeLogin(ChangeLoginRequest request)
    {
        var user = baseService.GetUserById(request.Id);

        if (request.Login.Length is < 4 or > 32)
            throw new IncorrectDataException("Login must be between 4 and 32 characters long!");
        
        if (await IsLoginUnique(request.Login))
            throw new IncorrectDataException("There is already a user with this login in the system");

        user.Login = request.Login;
        user.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(user);
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
    
    private async Task<bool> IsLoginUnique(string login)
    {
        var user = await repository.Get<User>(model => model.Login == login).FirstOrDefaultAsync();
        
        return user != null;
    }
    
    public async Task<IEnumerable<Process>> GetUserProcesses(Guid userId)
    {
        var companies = repository.GetAll<Company>()
            .Where(c => c.User.Id == userId);

        var processes = companies
            .SelectMany(c => repository.GetAll<Process>().Where(p => p.Company.Id == c.Id).Include(p=>p.Company));

        return await Task.FromResult(processes);
    }

    public async Task<IEnumerable<IndicatorDto>> GetIndicatorsOfProcess(Guid processId)
    {
        var entities = repository.GetAll<Indicator>()
                .Where(i => i.Process.Id == processId).AsQueryable();
        var dtos = mapper.Map<IEnumerable<IndicatorDto>>(entities);
        
        return dtos;
    }
    
    public async Task DeleteIndicatorByIdAsync(Guid id)
    {
        var entity = await repository.Get<Indicator>(e => e.Id == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException($"{nameof(Process)} {CommonStrings.NotFoundResult}");

        await repository.Delete(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Comment>> GetCommentsByProcessId(Guid processId)
    {
        var entities = repository.GetAll<Comment>()
            .Where(i => i.Process.Id == processId)
            .Include(c => c.User).AsQueryable();

        return entities;
    }
    
    public async Task<IEnumerable<Record>> GetRecordsByIndicatorId(Guid indicatorId)
    {
        var entities = repository.GetAll<Record>()
            .Where(i => i.Indicator.Id == indicatorId)
            .Include(c => c.Indicator).AsQueryable();

        return entities;
    }
}