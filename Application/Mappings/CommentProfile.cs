namespace Application.Mappings;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.Process.Id));

        CreateMap<CommentDto, Comment>()
            .ForMember(dest => dest.Process, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}