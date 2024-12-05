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
    
    public async Task DeleteCompany(Guid id)
    {
        var company = GetCompanyById(id);
        
        await repository.Delete(company);
        await repository.SaveChangesAsync();
    }
    
    public async Task DeleteProcess(Guid id)
    {
        var process = GetProcessById(id);
        
        await repository.Delete(process);
        await repository.SaveChangesAsync();
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
    
    private Company GetCompanyById(Guid id)
    {
        var company = repository.Get<Company>(model => model.Id == id).FirstOrDefault();
        if (company == null)
            throw new IncorrectDataException("There is not company with this Id");
        
        return company;
    }

    public Process GetProcessById(Guid id)
    {
        var process = repository.Get<Process>(model => model.Id == id).Include(process1 => process1.Company).FirstOrDefault();
        if (process == null)
            throw new IncorrectDataException("There is not process with this Id");
        
        return process;
    }
    
    public async Task<UserDto> PutUserAsync(UserDto dto)
    {
        var existingUser = repository.Get<User>(e => e.Id == dto.Id).FirstOrDefault();
        if (existingUser == null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        mapper.Map(dto, existingUser);
        existingUser.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(existingUser);
        await repository.SaveChangesAsync();

        return dto;
    }
}