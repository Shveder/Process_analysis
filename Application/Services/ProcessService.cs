namespace Application.Services;

[AutoInterface]
public class ProcessService(IDbRepository repository, IMapper mapper) : IProcessService
{
    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await repository.Get<Process>(e => e.Id == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException($"{nameof(Process)} {CommonStrings.NotFoundResult}");

        await repository.Delete(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<ProcessDto>> GetAllAsync()
    {
        var entities = repository.GetAll<Process>().Include(process => process.Company).AsQueryable();
        var dtos = mapper.Map<IEnumerable<ProcessDto>>(entities);
        
        return dtos;
    }
    
    public async Task<ProcessDto> GetByIdAsync(Guid id)
    {
        var entity = await repository.Get<Process>(e => e.Id == id)
            .FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        var dto = mapper.Map<ProcessDto>(entity);
       
        return dto;
    }
    
    public async Task<ProcessDto> PostAsync(ProcessDto dto)
    {
        var process = mapper.Map<Process>(dto);
        process.Company = GetCompanyById(dto.CompanyId);

        await repository.Add(process);
        await repository.SaveChangesAsync();
        
        return mapper.Map<ProcessDto>(process);
    }
    
    public async Task<ProcessDto> PutAsync(ProcessDto dto)
    {
        var existingProcess = repository.Get<Process>(e => e.Id == dto.Id).FirstOrDefault();
        if (existingProcess == null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        mapper.Map(dto, existingProcess);
        existingProcess.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(existingProcess);
        await repository.SaveChangesAsync();

        return dto;
    }
    
    public Company GetCompanyById(Guid id)
    {
        var company = repository.Get<Company>(model => model.Id == id).FirstOrDefault();
        if (company == null)
            throw new IncorrectDataException("There is not company with this Id");
        
        return company;
    }
}