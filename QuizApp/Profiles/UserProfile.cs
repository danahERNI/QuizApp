using AutoMapper;
using QuizApp.Data.DTO;
using QuizApp.Entities;

namespace QuizApp.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, CreateUserDTO>();

            CreateMap<LoginDTO, User>();
            CreateMap<User, LoginDTO>();

            CreateMap<PatchUserDTO, User>();
            CreateMap<User, PatchUserDTO>();

            CreateMap<PatchQuizDTO, Quiz>();
            CreateMap<Quiz, PatchQuizDTO>();

            CreateMap<CreateQuestionDTO, Question>()
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));
            CreateMap<Question, CreateQuestionDTO>();

            CreateMap<CreateQuizDTO, Quiz>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.QuestionDTO));
            CreateMap<Quiz, CreateQuizDTO>()
                .ForMember(dest => dest.QuestionDTO, opt => opt.MapFrom(src => src.Question));

            CreateMap<CreateChoiceDTO, Choice>();
            CreateMap<Choice, CreateChoiceDTO>();

            
        }
    }
}
