﻿namespace Application.Services.Base;

[AutoInterface]
public class BaseService(IDbRepository repository) : IBaseService
{

    public Process GetProcessById(Guid id)
    {
        var process = repository.Get<Process>(model => model.Id == id).Include(process1 => process1.Company).FirstOrDefault();
        if (process == null)
            throw new IncorrectDataException("There is not process with this Id");
        
        return process;
    }
    
    public User GetUserById(Guid id)
    {
        var user = repository.Get<User>(model => model.Id == id).FirstOrDefault();
        if (user == null)
            throw new IncorrectDataException("There is no user with this id");
        
        return user;
    }
    
    public Indicator GetIndicatorById(Guid id)
    {
        var indicator = repository.Get<Indicator>(model => model.Id == id)
            .Include(i => i.Process).FirstOrDefault();
        if (indicator == null)
            throw new IncorrectDataException("There is no indicator with this id");
        
        return indicator;
    }
    
    public Company GetCompanyById(Guid id)
    {
        var company = repository.Get<Company>(model => model.Id == id).FirstOrDefault();
        if (company == null)
            throw new IncorrectDataException("There is no company with this id");
        
        return company;
    }
}