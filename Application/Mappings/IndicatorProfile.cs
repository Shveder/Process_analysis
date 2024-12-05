namespace Application.Mappings;

public class IndicatorProfile : Profile
{
    public IndicatorProfile()
    {
        CreateMap<Indicator, IndicatorDto>()
            .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.Process.Id));

        CreateMap<IndicatorDto, Indicator>()
            .ForMember(dest => dest.Process, opt => opt.Ignore());
    }
}