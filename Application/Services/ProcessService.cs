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
        var entities = repository.GetAll<Process>().AsQueryable();
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
        var company = mapper.Map<Process>(dto);

        await repository.Add(company);
        await repository.SaveChangesAsync();
        
        return mapper.Map<ProcessDto>(company);
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
}