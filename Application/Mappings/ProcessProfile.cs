namespace Application.Mappings;

public class ProcessProfile : Profile
{
    public ProcessProfile()
    {
        CreateMap<Process, ProcessDto>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id));

        CreateMap<ProcessDto, Process>()
            .ForMember(dest => dest.Company, opt => opt.Ignore());
    }
}