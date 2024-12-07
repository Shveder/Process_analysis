namespace Application.Mappings;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}