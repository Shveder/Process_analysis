namespace Application.Services;

[AutoInterface]
public class AdminService(IDbRepository repository, IMapper mapper) : IAdminService
{
    public async Task<IQueryable<User>> GetAllUsers()
    {
        return await Task.FromResult(repository.GetAll<User>());
    }

    public async Task<IQueryable<Company>> GetAllCompanies()
    {
        return await Task.FromResult(repository.GetAll<Company>());
    }

    public async Task<IQueryable<LoginHistory>> GetUserLoginHistory(Guid id)
    {
        var user = GetUserById(id);
        
        return await Task.FromResult(repository.Get<LoginHistory>(model => model.User == user));
    }

    public async Task<IQueryable<RecentPassword>> GetUserPasswords(Guid id)
    {
        var user = GetUserById(id);
        
        return await Task.FromResult(repository.Get<RecentPassword>(model => model.User == user));
    }
    
    public async Task DeleteUser(Guid id)
    {
        var user = GetUserById(id);
        
        await repository.Delete(user);
        await repository.SaveChangesAsync();
    }
    
    private User GetUserById(Guid id)
    {
        var user =  repository.Get<User>(model => model.Id == id).FirstOrDefault();
        if (user == null)
            throw new IncorrectDataException("There is not user with this Id");
        
        return user;
    }
    
    public async Task<UserDto> ChangeRole(ChangeRoleRequest request)
    {
        var existingUser = repository.Get<User>(e => e.Id == request.Id).FirstOrDefault();
        if (existingUser == null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        existingUser.Role = request.Role;
        existingUser.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(existingUser);
        await repository.SaveChangesAsync();

        return mapper.Map<UserDto>(existingUser);
    }
    
    public async Task<UserDto> SetBlockStatus(SetBlockStatusRequest request)
    {
        var existingUser = repository.Get<User>(e => e.Id == request.Id).FirstOrDefault();
        if (existingUser == null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        existingUser.IsBlocked = request.Status;
        existingUser.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(existingUser);
        await repository.SaveChangesAsync();

        return mapper.Map<UserDto>(existingUser);
    }
}