namespace Application.Services;

[AutoInterface]
public class CompanyService(IDbRepository repository, IMapper mapper) : ICompanyService
{
    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await repository.Get<Company>(e => e.Id == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException($"{nameof(Company)} {CommonStrings.NotFoundResult}");

        await repository.Delete(entity);
        await repository.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var entities = repository.GetAll<Company>().AsQueryable();
        var dtos = mapper.Map<IEnumerable<CompanyDto>>(entities);
        
        return dtos;
    }
    
    public async Task<CompanyDto> GetByIdAsync(Guid id)
    {
        var entity = await repository.Get<Company>(e => e.Id == id)
            .FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        var dto = mapper.Map<CompanyDto>(entity);
       
        return dto;
    }
    
    public async Task<CompanyDto> PostAsync(CompanyDto dto)
    {
        var company = mapper.Map<Company>(dto);

        await repository.Add(company);
        await repository.SaveChangesAsync();
        
        return mapper.Map<CompanyDto>(company);
    }
    
    public async Task<CompanyDto> PutAsync(CompanyDto dto)
    {
        var existingCompany = repository.Get<Company>(e => e.Id == dto.Id).FirstOrDefault();
        if (existingCompany == null)
            throw new EntityNotFoundException(CommonStrings.NotFoundResult);
        
        mapper.Map(dto, existingCompany);
        existingCompany.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(existingCompany);
        await repository.SaveChangesAsync();

        return dto;
    }
}