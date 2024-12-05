namespace Application.Mappings;

public class RecordProfile : Profile
{
    public RecordProfile()
    {
        CreateMap<Record, RecordDto>()
            .ForMember(dest => dest.IndicatorId, opt => opt.MapFrom(src => src.Indicator.Id));

        CreateMap<RecordDto, Record>()
            .ForMember(dest => dest.Indicator, opt => opt.Ignore());
    }
}