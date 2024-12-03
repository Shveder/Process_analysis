namespace Application.Mappings;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();
    }
}